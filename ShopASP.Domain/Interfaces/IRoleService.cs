using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Domain.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetRoles();
    }
}
