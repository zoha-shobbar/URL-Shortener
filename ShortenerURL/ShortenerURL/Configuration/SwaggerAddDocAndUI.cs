using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ShortenerURL.Configuration
{
    public static class SwaggerAddDocAndUI
    {

        public static void AddSwaggerService(this IServiceCollection service)
        {

            service.AddSwaggerGen(s =>
            {

                s.SwaggerDoc(
                    "v1",
                    new OpenApiInfo()
                    {

                        Title = "Api Descriptor",
                        Version = "v1"
                    }

                    );
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //s.IncludeXmlComments(xmlPath);


            });

        }

        public static void AddSwaggerMiddlwares(this IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {

                s.SwaggerEndpoint("/swagger/v1/swagger.json", "WrestlingApi");

            });


        }

    }
}
