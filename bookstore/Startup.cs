using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using bookstore.Models;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Migrations;
using bookstore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace bookstore
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddRazorPages();
            //mm Dependencies
            services.AddMvc();
            services.AddScoped<IBookRepository<Author>, AuthorDbRepository>();
            services.AddScoped<IBookRepository<Book>, BookDbRepository>();
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddDbContext<BookStoreDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("SqlCon"));
            });
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
             else
             {
                 app.UseExceptionHandler("/Error");
                 // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                 app.UseHsts();
             }
            ;
            app.UseStaticFiles();
             app.UseHttpsRedirection();
             app.UseStaticFiles();

             app.UseRouting();

             app.UseAuthorization();

             app.UseEndpoints(endpoints =>
             {
                 endpoints.MapRazorPages();
             });

            app.UseMvc(route => {
                route.MapRoute("default", "{controller=Book}/{action=Index}/{id?}");
            });
            app.UseMvcWithDefaultRoute();
        }
    }
}
