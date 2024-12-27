using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class OrderProductDTO
    {
        public int dOrderProductId { get; set; }
        public int dOrderId { get; set; }
        public int dProductId { get; set; }
        public string dProductName { get; set; }
        public decimal dProductCost { get; set; }
        public int dQuantity { get; set; }
    }
}
