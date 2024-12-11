namespace ShopASP.Application.DTO
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderProductDTO> OrderProducts { get; set; }
        public OrderDTO()
        {
            OrderProducts = new List<OrderProductDTO>();
        }
    }
}
