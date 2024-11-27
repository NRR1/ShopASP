using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class OrderDTO
    {
        public int dOrderID { get; set; }
        public int dUserID { get; set; }
        public string dUser { get; set; }
        public int dProductID { get; set; }
        public string dProduct { get; set; }
    }
}
