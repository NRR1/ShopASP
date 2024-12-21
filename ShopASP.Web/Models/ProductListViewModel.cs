using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class ProductListViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public static ProductListViewModel FromDTO(ProductDTO dto)
        {
            return new ProductListViewModel
            {
                ID = dto.pdID,
                Name = dto.pdName,
                Description = dto.pdDescription,
                Cost = dto.pdCost,
                Quantity = dto.pdQuantity
            };
        }
    }
}
