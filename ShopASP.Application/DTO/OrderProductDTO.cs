using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class OrderProductDTO
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }

        // Конструктор
        public OrderProductDTO()
        {
            // Инициализация коллекций или любых других значений, если необходимо
        }
    }


}
