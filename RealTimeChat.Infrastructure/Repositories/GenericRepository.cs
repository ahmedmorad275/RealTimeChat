using Microsoft.EntityFrameworkCore;
using RealTimeChat.Application.Interfaces.Repositories;
using RealTimeChat.Infrastructure.Data;

namespace RealTimeChat.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _set;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _set.AddAsync(entity);
        }

        public virtual void Delete(T entity)
        {
            _set.Remove(entity);
        }

        public virtual async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _set.FindAsync(id) != null;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _set.FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            _set.Update(entity);
        }
    }
}