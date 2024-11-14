namespace ShopASP.Domain.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
