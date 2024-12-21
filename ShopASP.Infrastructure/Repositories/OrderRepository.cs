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
        public async Task<IEnumerable<Order>> GetAll()
        {
            List<Order> orders = await db.Orders.ToListAsync();
            return orders;
        }
        public async Task<Order> GetByID(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            return order;
        }
        public async Task Create(Order entity)
        {
            db.Orders.Add(entity);
            await db.SaveChangesAsync();
        }
        public async Task Update(Order order)
        {
            db.Orders.Update(order);
            await db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            Order? del = db.Orders.Find(id);
            db.Orders.Remove(del);
            await db.SaveChangesAsync();
        }
    }
}
