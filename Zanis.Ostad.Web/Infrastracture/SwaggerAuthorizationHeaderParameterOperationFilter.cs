using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zanis.Ostad.Web.Infrastracture
{
    public class SwaggerAuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters=new List<IParameter>();

            operation.Parameters.Add(new Parameter
            {
                Name = "Authorization",
                In = "header",
                Description = "access token",
                Required = false
            });
        }
    }

    public class Parameter : IParameter
    {
        public string Name { get; set; }
        public string In { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public Dictionary<string, object> Extensions { get; } = new Dictionary<string, object>();
    }
}
