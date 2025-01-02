namespace ShopASP.Web.Models
{
    public class OrderProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost => ProductCost * Quantity;
    }
}
