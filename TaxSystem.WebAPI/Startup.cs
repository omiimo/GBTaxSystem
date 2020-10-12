using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.AspNetCore.Middlewares;
using TaxSystem.Application;
using TaxSystem.WebAPI.Filters;

namespace TaxSystem.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Adds application services (TaxCalculator Service)
            services.AddApplication();

            services.AddControllers(options=>
            options.Filters.Add(new ApiExceptionFilter()));

            // Register the Swagger services
            services.AddSwaggerDocument(config => 
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Purchase Data Calculator - Austria";
                    document.Info.Description = "Calculate Net, Gross, VAT amounts for your purchases in Austria. Valid VAT rates are: 10%, 13%, 20%";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Omprakash",
                        Email = "omi@live.in"
                    };
                };
            });

        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
