using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class OrderProductViewModel
    {
        public int OrderProductID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }


        public OrderProductDTO ToDTO()
        {
            return new OrderProductDTO
            {
                dOrderProductId = OrderProductID,
                dOrderId = OrderID,
                dProductId = ProductID,
                dProductName = ProductName,
                dProductCost = ProductCost
            };
        }

        public static OrderProductViewModel FromDTO(OrderProductDTO dto)
        {
            return new OrderProductViewModel
            {
                OrderProductID = dto.dOrderProductId,
                OrderID = dto.dOrderId,
                ProductID = dto.dProductId,
                ProductName = dto.dProductName,
                ProductCost = dto.dProductCost
            };
        }
    }
}
