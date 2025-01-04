using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Infrastructure.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly ShopASPDBContext _dbContext;

        public OrderProductRepository(ShopASPDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Метод для добавления записи в таблицу OrderProduct
        public async Task Create(OrderProduct orderProduct)
        {
            await _dbContext.OrderProducts.AddAsync(orderProduct);
            await _dbContext.SaveChangesAsync();
        }

        // Метод для получения всех записей из OrderProduct
        public async Task<IEnumerable<OrderProduct>> GetAll()
        {
            return await _dbContext.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .ToListAsync();
        }

        // Метод для получения записи по OrderID и ProductID
        public async Task<OrderProduct> GetById(int orderId, int productId)
        {
            return await _dbContext.OrderProducts
                .FirstOrDefaultAsync(op => op.OrderID == orderId && op.ProductID == productId);
        }

        // Метод для удаления связи между заказом и продуктом
        public async Task Delete(int orderId, int productId)
        {
            var orderProduct = await GetById(orderId, productId);
            if (orderProduct != null)
            {
                _dbContext.OrderProducts.Remove(orderProduct);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
