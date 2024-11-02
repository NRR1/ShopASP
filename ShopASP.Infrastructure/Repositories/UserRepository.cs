using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Infrastructure.Repositories
{
    public class UserRepository : GenericInterface<User>
    {
        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<User> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
        public Task VerifyAsync(User entity)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
