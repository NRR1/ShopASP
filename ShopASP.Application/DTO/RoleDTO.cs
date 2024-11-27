namespace ShopASP.Application.DTO
{
    public class RoleDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<int> UserIDs { get; set; }
    }
}
