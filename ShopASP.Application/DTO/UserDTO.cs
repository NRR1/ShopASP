using ShopASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class UserDTO
    {
        //future
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Pathronomic { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int RoleID { get; set; }
        public string? RoleName { get; set; }
    }
}
