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
    public class ProductRepository : IProductReposutory
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
            Product product = await db.Products.FindAsync(id);
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
            await db.Products.AddAsync(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task UpdateProduct(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
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
            var delProduct = await db.Products.FindAsync(id);
            db.Products.Remove(delProduct);
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
