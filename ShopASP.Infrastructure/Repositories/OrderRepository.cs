using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;

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
            var orders = await db.Orders.ToListAsync();
            return orders;
        }

        public async Task<Order> GetByIDAsync(int id)
        {
            var order = await db.Orders.FindAsync(id);
            return order;
        }

        public async Task CreateAsync(Order order)
        {
            db.Orders.Add(order);
            await db.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            db.Orders.Update(order);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Order? dOrder = await db.Orders.FindAsync(id);
            if(dOrder == null)
            {
                Console.WriteLine("order is null");
            }
            db.Orders.Remove(dOrder);
            await db.SaveChangesAsync();
        }
    }
}
