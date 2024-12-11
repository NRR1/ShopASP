using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;

namespace ShopASP.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopASPDBContext db;
        public ProductRepository(ShopASPDBContext _db)
        {
            db = _db;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            List<Product> products = await db.Products.ToListAsync();
            try
            {
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public async Task<Product> GetByIDAsync(int id)
        {
            Product? product = await db.Products.FindAsync(id);
            try
            {
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public async Task CreateAsync(Product entity)
        {
            db.Products.Add(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task UpdateAsync(Product product)
        {
            db.Products.Update(product);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task DeleteAsync(int id)
        {
            Product? dP = await db.Products.FindAsync(id);
            db.Products.Remove(dP);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
