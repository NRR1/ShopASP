namespace ShopASP.Web.Models
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public List<OrderProductViewModel> Products { get; set; }
    }
}
