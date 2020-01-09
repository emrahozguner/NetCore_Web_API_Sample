using API.Domain.Models;
using API.Persistence.Contexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<AppDbContext>())
            {
                EnsureSeedData(context);
            }

            host.Run();
        }

        public static void EnsureSeedData(AppDbContext context)
        {
            CategorySeed(context);
            ProductSeed(context);

            context.SaveChanges();
        }

        private static void CategorySeed(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category {Id = 100, Name = "Fruits and Vegetables"});
                context.Categories.Add(new Category { Id = 101, Name = "Dairy" });
            }
        }

        private static void ProductSeed(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.Add(new Product
                {
                    Id = 100,
                    Name = "Apple",
                    QuantityInPackage = 1,
                    UnitOfMeasurement = EUnitOfMeasurement.Unity,
                    CategoryId = 100
                });
                context.Products.Add(new Product
                {
                    Id = 101,
                    Name = "Milk",
                    QuantityInPackage = 2,
                    UnitOfMeasurement = EUnitOfMeasurement.Liter,
                    CategoryId = 101
                });
            }
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                //.UseSerilog((hostingContext, loggerConfiguration) => SerilogConfigurator.ConfigureLogger(hostingContext.Configuration, loggerConfiguration))
                .UseStartup<Startup>()
                .Build();
        }
    }
}