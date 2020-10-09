using Microsoft.EntityFrameworkCore;
using SalesProductApi.Models;

namespace SalesProductApi.DB
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}