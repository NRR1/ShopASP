using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Domain.Interfaces
{
    public interface GenericInterface<T> where T : class
    {
        Task<T> GetByID(int id);
        Task Create(T entity);
        Task Delete(int id);
    }
}
