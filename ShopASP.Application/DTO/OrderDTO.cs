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
        public string dUserID { get; set; }
        public DateTime dOrderDate { get; set; }
        public int dTotalAmount { get; set; }

        public List<OrderProductDTO> dOrderProducts { get; set; }
    }
}
