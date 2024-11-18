using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class RegisterUserDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pathronomic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
