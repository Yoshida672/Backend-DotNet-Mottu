using CP2_BackEndMottu_DotNet.Domain.Entity;

namespace CP2_BackEndMottu_DotNet.Domain.Interface
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid Id);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void Update(T entity);

        void Delete(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}