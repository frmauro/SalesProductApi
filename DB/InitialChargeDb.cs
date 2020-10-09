
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using SalesProductApi.Models;

namespace SalesProductApi.DB
{
    public static class InitialChargeDb
    {
        public static void InsertDataInDB(IApplicationBuilder app) 
         {
            InsertDataInDB(app.ApplicationServices.GetRequiredService<ProductContext>());
         }
        public static void InsertDataInDB(ProductContext context)
        {
            System.Console.WriteLine("Aplication Migrations...");
            context.Database.Migrate();
            if (!context.Products.Any()) 
            {
                System.Console.WriteLine("Creating data...");
                context.Products.AddRange(
                    new Product("Luvas de goleiro", 25, 200, ProductStatus.Active),
                    new Product("Bola de basquete", 225, 100, ProductStatus.Active),
                    new Product("Bola de Futebol", 125, 800, ProductStatus.Active),
                    new Product("Óculos para natação", 325, 600, ProductStatus.Active),
                    new Product("Meias Grandes", 28, 400, ProductStatus.Active),
                    new Product("Calção de banho", 55, 270, ProductStatus.Active),
                    new Product("Cesta para quadra", 75, 250, ProductStatus.Active)
                );
                context.SaveChanges();
            } else {
                System.Console.WriteLine("Already exists data...");
            }
        }
    }
}