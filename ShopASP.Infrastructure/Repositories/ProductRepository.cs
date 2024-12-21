using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopASPDBContext db;
        public ProductRepository(ShopASPDBContext _db)
        {
            db = _db;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            IEnumerable<Product> products = await db.Products.ToListAsync();
            return products;
        }
        public async Task<Product> GetByID(int id)
        {
            Product? product = await db.Products.FindAsync(id);
            return product;
        }
        public async Task Create(Product entity)
        {
            await db.Products.AddAsync(entity);
            await db.SaveChangesAsync();
        }
        public async Task Update(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            Product? product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
        }
    }
}
