using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Extensions;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum || (Nullable.GetUnderlyingType(context.Type)?.IsEnum ?? false))
        {
            var enumType = context.Type.IsEnum ? context.Type : Nullable.GetUnderlyingType(context.Type);
            schema.Enum.Clear();
            foreach (var name in Enum.GetNames(enumType))
            {
                schema.Enum.Add(new OpenApiString(name));
            }
            schema.Type = "string";
            schema.Format = null;
        }
    }
}