using System.ComponentModel.DataAnnotations;

namespace ShopASP.Web.Models
{
    public class DetailProductViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Количество")]
        public int Quantity { get; set; }
    }
}
