namespace ShopASP.Domain.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Pathronomic { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int RoleID { get; set; }
        public virtual Role? Roles { get; set; }
    }
}
