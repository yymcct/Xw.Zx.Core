using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Config
{
    public class AuthorizeCheckOperationFilter: IOperationFilter
    {
        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<IParameter>();
            var info = context.MethodInfo;
            context.ApiDescription.TryGetMethodInfo(out info);
            try
            {
                Attribute attribute = info.GetCustomAttribute(typeof(AuthorizeAttribute));
                if (attribute != null)
                {
                    operation.Parameters.Add(new BodyParameter
                    {
                        Name = "Authorization",
                        @In = "header",
                        Description = "access_token",
                        Required = true,
                    });
                }

            }
            catch
            { }
        }
    }
}
