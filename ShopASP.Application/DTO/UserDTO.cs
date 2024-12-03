namespace ShopASP.Application.DTO
{
    public class UserDTO
    {
        public int dID { get; set; }
        public string dSurname { get; set; }
        public string dPathronomic { get; set; }
        public string dEmail { get; set; }
        public ICollection<int> dRoleID { get; set; }
        public ICollection<string> dRoleNames { get; set; }

        public UserDTO()
        {
            dRoleID = new HashSet<int>();
            dRoleNames = new HashSet<string>();
        }
    }
}
