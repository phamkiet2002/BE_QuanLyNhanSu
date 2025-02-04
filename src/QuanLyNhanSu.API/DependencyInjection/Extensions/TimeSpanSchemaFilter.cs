using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace QuanLyNhanSu.API.DependencyInjection.Extensions;

public class TimeSpanSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(TimeSpan))
        {
            schema.Type = "string";
            schema.Format = "time-span";
            schema.Example = new OpenApiString("08:00:00");
        }
    }
}
