using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Domain.Interfaces
{
    public interface GenericInterface<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIDAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task VerifyAsync(T entity);
        Task DeleteAsync(int id);
    }
}
