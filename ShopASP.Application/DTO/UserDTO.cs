namespace ShopASP.Application.DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Pathronomic { get; set; }
        public string Email { get; set; }
        public ICollection<int> RoleIDs { get; set; }
        public ICollection<string> RoleNames { get; set; }
    }
}
