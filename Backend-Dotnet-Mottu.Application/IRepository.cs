namespace Backend_Dotnet_Mottu.Application
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(long Id);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(long id);
        Task<(IEnumerable<T> Items, int TotalItems)> GetPaginatedAsync(int page, int pageSize);

    }
}