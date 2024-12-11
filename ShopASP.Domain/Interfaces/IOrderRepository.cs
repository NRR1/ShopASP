namespace ShopASP.Domain.Interfaces
{
    public interface IOrderRepository : GenericInterface<Order>
    {
        Task UpdateOrder(Order order);
    }
}
