using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleWebAPI.config;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(config =>
            {
                var swaggerConfig = Configuration.GetSection("Swagger").Get<Swagger>();
                config.SwaggerDoc(swaggerConfig.APIVersion, new Info {
                    Version = swaggerConfig.APIVersion,
                    Title = swaggerConfig.Title,
                    Description = swaggerConfig.Description,
                    TermsOfService = swaggerConfig.TermsOfService

                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            var swaggerConfig = Configuration.GetSection("Swagger").Get<Swagger>();

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(config => {
                config.SwaggerEndpoint($"/swagger/{swaggerConfig.APIVersion}/swagger.json", $"My API {swaggerConfig.APIVersion.ToUpper()}");
            });
        }
    }
}
