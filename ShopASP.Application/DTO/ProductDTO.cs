using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class ProductDTO
    {
        public int pID { get; set; }
        public string pName { get; set; }
        public string pDescription { get; set; }
        public int pCost { get; set; }
        public int pQuantity { get; set; }
        public string pOrders { get; set; }
    }
}
