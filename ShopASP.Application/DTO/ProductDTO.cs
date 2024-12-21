using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class ProductDTO
    {
        public int pdID { get; set; }
        public string pdName { get; set; }
        public string pdDescription { get; set; }
        public decimal pdCost { get; set; }
        public int pdQuantity { get; set; }
    }
}
