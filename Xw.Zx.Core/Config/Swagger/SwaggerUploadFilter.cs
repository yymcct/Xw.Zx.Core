using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Config
{
    public class SwaggerUploadFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {



            #region Swagger授权处理
            //if (operation.Security == null)
            //{
            //    operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
            //}
            //else
            //{
            //    operation.Security.Add(new Dictionary<string, IEnumerable<string>>
            //                            {

            //                                  {"oauth2", new List<string> { "openid", "profile", "userservicesapi" }}
            //                            });
            //}
            #endregion


            #region Swagger 文件上传处理

            if (!context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) &&
             !context.ApiDescription.HttpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

           // var fileParameters = context.ApiDescription.ActionDescriptor.Parameters.Where(n => n.ParameterType == typeof(IFormFile)).ToList();
            var fileParameters = context.ApiDescription.ActionDescriptor.Parameters.Where(n => n.ParameterType == typeof(IFormCollection)).ToList();
            if (fileParameters.Count > 0)
            {
                for (int i = 0; i < fileParameters.Count; i++)
                {
                    if (i == 0)
                    {
                        operation.Parameters.Clear();
                    }
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Name = fileParameters[i].Name,
                        In = "formData",
                        Description = "Upload File",
                        Required = true,
                        Type = "file"
                    });

                }

                operation.Consumes.Add("multipart/form-data");
            }
            #endregion
        }
    }
}
