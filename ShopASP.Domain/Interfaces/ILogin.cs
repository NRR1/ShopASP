using ShopASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Domain.Interfaces
{
    public interface ILogin<T2>
    {
        Task<T2> Login(string login, string password);
        Task<T2> Register(T2 user);
        Task<bool> Verify(int id);
        Task<bool> ReserPassword(int id, string nPassword);

    }
}