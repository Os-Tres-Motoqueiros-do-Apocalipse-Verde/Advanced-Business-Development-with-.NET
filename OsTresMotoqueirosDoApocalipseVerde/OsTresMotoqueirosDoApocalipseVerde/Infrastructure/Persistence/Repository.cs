using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Context;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Persistence
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MottuContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(MottuContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);
    }
}