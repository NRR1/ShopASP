using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class ProductViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
        public int Orders { get; set; }
    }
}
