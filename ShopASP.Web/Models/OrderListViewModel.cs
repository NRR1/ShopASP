using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class OrderListViewModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public static OrderListViewModel FromDTO(OrderDTO dto)
        {
            return new OrderListViewModel
            {
                ID = dto.dOrderID,
                UserID = dto.dUserID,
                OrderDate = dto.dOrderDate,
                TotalAmount = dto.dTotalAmount
            };
        }
    }
}
