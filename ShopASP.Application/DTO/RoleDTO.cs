using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class RoleDTO
    {
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
