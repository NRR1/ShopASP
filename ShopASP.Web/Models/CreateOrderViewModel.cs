namespace ShopASP.Web.Models
{
    public class CreateOrderViewModel
    {
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderProductViewModel> OrderProducts { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
