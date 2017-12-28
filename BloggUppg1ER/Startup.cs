using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BloggUppg1ER.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggUppg1ER
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // I specify that I will use a MVC application
            services.AddMvc();

            // We add this to enstablish the connection between DB and EF
            var connection = @"Server=EGILENOVO\SQLEXPRESS;Database=BlogER;Trusted_Connection=True;ConnectRetryCount=0";

            // Note: I moved the Context class in DataAccess folder. The namespace is still 'BloggUppg1ER.Models'
            services.AddDbContext<BlogERContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // It means that we have CSS, image files under wwwroot subdir!
            app.UseStaticFiles();

            // To give a body page to error pages
            app.UseStatusCodePages();

            // I remove the {?id} part in the routing because we are not sending parameters 
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "Default",
                template: "{controller=Home}/{action=CreatePost}/{id?}"
                );
            });
        }
    }
}
