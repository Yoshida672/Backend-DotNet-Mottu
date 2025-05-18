using CP2_BackEndMottu_DotNet.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(MotoContext entity)
        {

            _context = entity;
            _dbSet = _context.Set<T>();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}