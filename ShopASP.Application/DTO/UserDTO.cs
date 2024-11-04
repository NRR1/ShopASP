using ShopASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.DTO
{
    public class UserDTO
    {
        //future
        public int ID { get; set; }
        [Display(Name = "Имя")]
        public string? Name { get; set; }
        [Display(Name = "Фамилия")]
        public string? Surname { get; set; }
        [Display(Name = "Отчество")]
        public string? Pathronomic { get; set; }
        [Display(Name = "Логин")]
        public string? Login { get; set; }
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
        [Display(Name = "Роль id")]
        public int RoleID { get; set; }
        [Display(Name = "Роль пользователя")]
        public string? RoleName { get; set; }
    }
}
