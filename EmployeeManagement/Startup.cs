using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Database;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
            options => options.UseSqlServer(_config.GetConnectionString("MyResumeTrackerDBConnection")));

            services.AddMvc().AddXmlSerializerFormatters();

             services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
             services.AddSingleton<IJobRepository, MockJobRepository>();
            //services.AddTransient<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddTransient<IJobRepository, MockJobRepository>();
           // services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddScoped<IJobRepository, MockJobRepository>();
            //services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            //services.AddScoped<IJobRepository, SQLJobRespository>();

            services.AddMvc(options => options.EnableEndpointRouting = false);
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            //Pipeline items below
            if (env.IsDevelopment())
            {    //turn off this pipeline request
                 //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                 //{
                //    SourceCodeLineCount = 20
                //};
                //app.UseDeveloperExceptionPage(developerExceptionPageOptions);
                app.UseDeveloperExceptionPage();


            }
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            //app.UseMvc();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            //}
            //);

            app.UseMvc(routes =>
            {
                
                routes.MapRoute("Jobs", "{controller=Job}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

          
            //app.UseFileServer();


            // app.UseStaticFiles();
            // app.UseFileServer(); this can be used to replace app.UserDefaultFiels and app.useDefaultfiles. Need to create fileServerOptions object instead.
            // app.UseRouting();

            //chaining middleware
            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("MW1: incoming request");
            //    //await context.Response.WriteAsync("Hello from 1st Middleware.");
            //    // Do work that doesn't write to the Response.
            //    await next();
            //    // Do logging or other work that doesn't write to the Response.
            //    logger.LogInformation("MW1: outgoing request");
            //});

            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("MW2: incoming request");
            //    //await context.Response.WriteAsync("Hello from 1st Middleware.");
            //    // Do work that doesn't write to the Response.
            //    await next();
            //    // Do logging or other work that doesn't write to the Response.
            //    logger.LogInformation("MW2: outgoing request");
            //});

            ////app.Run(async context =>
            ////{
            ////    //throw new Exception("some error processing the request");
            ////    //await context.Response.WriteAsync("hello from startup");
            ////    //logger.LogInformation("MW3: Hello from 2nd middleware");

            ////    //get value from launchSettings.json
            ////    await context.Response.WriteAsync("Hello MyResumeTracker");
            ////});

            //Original code from project start
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("First Middleware ");

            //        //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            //        //await context.Response.WriteAsync(_config["Mykey"]);  //reads from appsettings.json
            //    });


            // });


        }
    }
}
