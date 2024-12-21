using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }

        public ProductDTO ToDTO()
        {
            return new ProductDTO
            {
                pdID = ID,
                pdName = Name,
                pdDescription = Description,
                pdCost = Cost,
                pdQuantity = Quantity
            };
        }

        public static ProductViewModel FromDTO(ProductDTO dto)
        {
            return new ProductViewModel
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
