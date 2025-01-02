using ShopASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Domain.Interfaces
{
    public interface IProductRepository : GenericInterface<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task Update(Product product);
    }
}
