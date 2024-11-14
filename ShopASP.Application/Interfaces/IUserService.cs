using ShopASP.Application.DTO;
using ShopASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.Interfaces
{
    public interface IUserService : GenericInterface<UserDTO>
    {
        Task UpdateUser(UserDTO userDTO);
        Task VerifyUser(UserDTO userDTO);
    }
}
