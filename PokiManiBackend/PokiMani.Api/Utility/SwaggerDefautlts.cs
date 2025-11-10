using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PokiMani.Api.Utility
{
    public class CommonResponseFilters : IOperationFilter
    {
        // This will apply some default possible response types to the OpenApi Spec using Swagger
        // It applies to ALL the endpoints, so I should maybe come back and add more logic to 
        // not add the 401 to the endpoints that aren't authenticated (eg. /auth/register)
        // Not crucial for now. 
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Responses.TryAdd("400", new OpenApiResponse { Description = "Bad Request" });
            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
        }
    }
}
