namespace ShopASP.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
    }
}
