using DataTransfer.BackgroundJobs;
using DataTransfer.BackgroundJobs.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataTransfer.Configurations;

public static class StartupConfiguration
{
    public static IServiceCollection AddBackGroundJobs(this IServiceCollection services)
    {
        services.AddScoped<IDbDataTransferJob, DbDataTransferJob>();

        return services;
    }
}