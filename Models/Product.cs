namespace Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public  ProductStatus Status { get; set; }
    }
}