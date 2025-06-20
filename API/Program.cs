using API.Extensions;
using Application.Caching;
using Application.Caching.Implementation;
using Application.Mappings;
using Application.Repositories;
using DataTransfer.Configurations;
using DataTransfer.Contexts;
using DataTransfer.MappingConfigs;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAll", builder => 
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddControllers();
var config = builder.Configuration;

var autoverseConnectionString = config.GetConnectionString("DataBase");
builder.Services.AddDbContextFactory<AutoVerseContext>(options =>
        options.UseNpgsql(autoverseConnectionString), ServiceLifetime.Transient);

var oldConnectionString = config.GetConnectionString("OldDataBase");
builder.Services.AddDbContext<OldAutoVerseContext>(options =>
    options.UseMySql(oldConnectionString,
        ServerVersion.AutoDetect(oldConnectionString)));

builder.Services.AddHangfire(x =>
    x.UsePostgreSqlStorage(config =>
    {
        config.UseNpgsqlConnection(autoverseConnectionString);
       
    },
    new PostgreSqlStorageOptions
    {
        SchemaName = "hangfire" 
    })
    .UseSerializerSettings(new JsonSerializerSettings
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        TypeNameHandling = TypeNameHandling.Objects
    })
    );

builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
        ConnectionMultiplexer.Connect(config.GetConnectionString("Redis")));
builder.Services.AddTransient<ICacheService ,CacheService>();
builder.Services.AddTransient<IViewCounterService ,ViewCounterService>();

builder.Services.AddHangfireServer();

builder.Services.AddBackGroundJobs();
builder.Services.AddTransient<IMarkRepository, MarkRepository>();
builder.Services.AddTransient<ICustomRequestsRepository, CustomRequestsRepository>();
DataMigrationMappingConfig.Register();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<EnumSchemaFilter>();
});
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationExtension>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AutoVerse API",
        Version = "v1",
        Description = "AutoVerse API Documentation"
    });
    
    // Include XML comments if they exist
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
    
    // Include all controllers from the API assembly
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "API.xml"));
});
builder.Services.AddMediator(o => o.ServiceLifetime = ServiceLifetime.Scoped);
MapConfig.Register();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetService<AutoVerseContext>();
    //dbContext.Database.Migrate();
    //scope.ServiceProvider.InitializeDataTransferJob();
}

app.UseCors("AllowAll");


app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoVerse API V1");
    c.RoutePrefix = "swagger";
    c.EnableDeepLinking();
    c.DisplayRequestDuration();
});



//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "AutoVerse API is running! Go to /swagger for API documentation");

app.Run();