namespace ShopASP.Domain.Entities
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
