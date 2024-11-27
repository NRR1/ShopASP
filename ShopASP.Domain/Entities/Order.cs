using ShopASP.Domain.Entities;

public class Order
{
    public int OrderID { get; set; }
    public int UserID { get; set; }
    public int ProductID { get; set; }
    public virtual User User { get; set; }
    public virtual Product Product { get; set; }
}
