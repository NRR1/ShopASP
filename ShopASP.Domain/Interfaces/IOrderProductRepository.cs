using ShopASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Domain.Interfaces
{
    public interface IOrderProductRepository
    {
        Task Create(OrderProduct orderProduct);  // Метод для добавления записи в OrderProduct
        Task<IEnumerable<OrderProduct>> GetAll();  // Метод для получения всех записей из OrderProduct
        Task<OrderProduct> GetById(int orderId, int productId);  // Метод для получения записи по OrderID и ProductID
        Task Delete(int orderId, int productId);  // Метод для удаления связи
    }
}
