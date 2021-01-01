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

             //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
             //services.AddSingleton<IJobRepository, MockJobRepository>();
            //services.AddTransient<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddTransient<IJobRepository, MockJobRepository>();
           // services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddScoped<IJobRepository, MockJobRepository>();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddScoped<IJobRepository, SQLJobRespository>();

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
            else
            {
                app.UseExceptionHandler("/Error");
                //Error is the Controller
                 app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            

            app.UseMvc(routes =>
            {
                
                routes.MapRoute("Jobs", "{controller=Job}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
