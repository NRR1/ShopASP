using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly ShopASPDBContext db;

    public ProductRepository(ShopASPDBContext _db)
    {
        db = _db;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await db.Products.ToListAsync();
    }

    public async Task<Product> GetByID(int id)
    {
        return await db.Products.FindAsync(id);
    }

    public async Task Create(Product entity)
    {
        await db.Products.AddAsync(entity);
        await db.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        db.Products.Entry(product).State = EntityState.Modified;
        await db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        Product? product = await db.Products.FindAsync(id);
        db.Products.Remove(product);
        await db.SaveChangesAsync();
    }
}
