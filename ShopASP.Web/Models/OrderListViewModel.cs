using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderProductDTO> Orders { get; set; }
    }
}
