using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
