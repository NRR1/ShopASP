using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;

public class OrderRepository : IOrderRepository
{
    private readonly ShopASPDBContext db;

    public OrderRepository(ShopASPDBContext _db)
    {
        db = _db;
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await db.Orders.Include(o => o.OrderProducts)
                              .ThenInclude(op => op.Product)
                              .ToListAsync();
    }

    public async Task<Order> GetByID(int id)
    {
        return await db.Orders.Include(o => o.OrderProducts)
                              .ThenInclude(op => op.Product)
                              .FirstOrDefaultAsync(x => x.ID == id);
    }

    public async Task Create(Order entity)
    {
        await db.Orders.AddAsync(entity);
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
