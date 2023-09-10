using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using bookstore.Models;


using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.Extensions.DependencyInjection;



namespace bookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var webhost= CreateHostBuilder(args).Build();

            //runmigration(webhost);
            webhost.Run();
        }

        
        /*private static void runmigration(IHost webhost)
        { using (var scope = webhost.Services.CreateScope())
            { var db = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();
                db.Database.Migrate();
            }
        }*/

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
