using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public OrderDTO ToDTO()
        {
            return new OrderDTO
            {
                dOrderID = OrderID,
                dUserID = UserID,
                dOrderDate = OrderDate,
                dTotalAmount = TotalAmount
            };
        }

        public static OrderViewModel FromDTO(OrderDTO dto)
        {
            return new OrderViewModel
            {
                OrderID = dto.dOrderID,
                UserID = dto.dUserID,
                OrderDate = dto.dOrderDate,
                TotalAmount = dto.dTotalAmount
            };
        }
    }
}