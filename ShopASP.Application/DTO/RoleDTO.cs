namespace ShopASP.Application.DTO
{
    public class RoleDTO
    {
        public int dID { get; set; }
        public string dName { get; set; }
        public ICollection<int> dUserID { get; set; }
        public RoleDTO()
        {
            dUserID = new HashSet<int>();
        }

    }
}
