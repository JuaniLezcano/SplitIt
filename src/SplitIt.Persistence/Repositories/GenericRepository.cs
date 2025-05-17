using SplitIt.Application.Interfaces;
using SplitIt.Domain.Common;

namespace SplitIt.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly SplitItDbContext _context;

        public GenericRepository(SplitItDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
                return;
                //throw new NotFoundException($"{typeof(T).Name} with id {id} not found");

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

}
