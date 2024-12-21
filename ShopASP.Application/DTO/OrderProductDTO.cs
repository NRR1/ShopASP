using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class OrderProductDTO
    {
        public int OrderProductId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public int Quantity { get; set; }
    }
}
