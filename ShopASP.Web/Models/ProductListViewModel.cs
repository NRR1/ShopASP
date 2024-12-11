using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class ProductListViewModel
    {
        public int ID { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
        public UserDTO User { get; set; }
    }
}
