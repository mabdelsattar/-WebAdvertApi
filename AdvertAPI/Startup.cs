using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertAPI.HealthChecks;
using AdvertAPI.Sevices;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdvertAPI
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
            services.AddAutoMapper(c => c.AddProfile<AdvertProfile>(), typeof(Startup));
         
            services.AddTransient<IAdvertStorageService, DynamoDBAdvertStorage>();
            

            services.AddControllersWithViews();
            services.AddHealthChecks()
           //.AddSqlServer(connectionString:)
           //.AddUrlGroup(new Uri("https://localhost:44383/Home/Privaecy"))
           .AddCheck("mo", new DBStorageService());
           //.AddFilePath("PATH_HERE", HealthStatus.Unhealthy, tags: new[] { "ready" }); // you can make it using extension methods





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //make sure that application is alife 
            //if use amazone dynamo Db we want to make checks for them 
            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
