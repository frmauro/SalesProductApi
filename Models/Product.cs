using System.ComponentModel.DataAnnotations;

namespace SalesProductApi.Models
{
    public class Product
    {

        public Product()
        {}

        public Product (string description=null, int amount=0, decimal price=0, ProductStatus status=ProductStatus.Active)
        {
            this.Description = description;
            this.Amount = amount;
            this.Price = price;
            this.Status = status;
         }

        [Key]
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
    }
}