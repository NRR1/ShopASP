using Microsoft.AspNetCore.Identity;
using ShopASP.Domain.Entities;

public class Order
{
    public int ID { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public DateTime OrderDate { get; set; }
    public int TotalAmount { get; set; }

    public ICollection<OrderProduct> OrderProducts { get; set; }
}
