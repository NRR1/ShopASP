namespace ShopASP.Domain.Interfaces
{
    public interface GenericInterface<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIDAsync(int id);
        Task CreateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
