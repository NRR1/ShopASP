using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class OrderProductListViewModel
    {
        public int OrderProductID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public int Quantity { get; set; }
        public static OrderProductListViewModel FromDTO(OrderProductDTO dto)
        {
            return new OrderProductListViewModel
            {
                OrderProductID = dto.dOrderProductId,
                OrderID = dto.dOrderId,
                ProductID = dto.dProductId,
                ProductName = dto.dProductName,
                ProductCost = dto.dProductCost,
                Quantity = dto.dQuantity,
            };
        }
    }
}
