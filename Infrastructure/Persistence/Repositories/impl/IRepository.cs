using CP2_BackEndMottu_DotNet.Domain.Entity;

namespace CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories.impl
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid Id);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<(IEnumerable<T> Items, int TotalItems)> GetPaginatedAsync(int page, int pageSize);

    }
}