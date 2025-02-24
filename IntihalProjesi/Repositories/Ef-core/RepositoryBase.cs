using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IntihalProjesi.Repositories.Ef_core
{
    public abstract class RepositoryBase<T> : IRepositorybase<T>
        where T : class
    {
        protected readonly OrjinalIntihalDbContext _context;

        public RepositoryBase(OrjinalIntihalDbContext context)
        {
            _context = context;

        }

        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context?.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
            
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
