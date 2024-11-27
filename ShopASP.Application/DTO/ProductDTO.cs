using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class ProductDTO
    {
        [Display(Name = "ID")]
        public int pID { get; set; }
        [Display(Name = "Название")] 
        public string pName { get; set; }
        [Display(Name = "Описание")]
        public string pDescription { get; set; }
        [Display(Name = "Цена")]
        public int pCost { get; set; }
        [Display(Name = "Количество")]
        public int pQuantity { get; set; }
        
    }
}
