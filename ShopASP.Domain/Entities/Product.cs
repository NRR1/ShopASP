namespace ShopASP.Domain.Entities
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
