using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopASPDBContext db;
        public OrderRepository(ShopASPDBContext _db)
        {
            db = _db;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            List<Order> orders = await db.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync();
            try
            {
                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public async Task<Order> GetByIDAsync(int id)
        {
            Order? order = await db.Orders
                                   .Where(o => o.UserId == id.ToString())
                                   .Include(o => o.OrderProducts)
                                   .ThenInclude(op => op.Product)
                                   .FirstOrDefaultAsync(x => x.ID == id);
            try
            {
                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public Task CreateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
        public Task UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
