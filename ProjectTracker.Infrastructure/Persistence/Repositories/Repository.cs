using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Infrastructure.Data;

namespace ProjectTracker.Infrastructure.Persistence.Repositories
{
    public class Repository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == (Guid)Convert.ChangeType(id, typeof(Guid)));
        }

        public async Task<T> FindAsync(
             Expression<Func<T, bool>> predicate,
             Func<IQueryable<T>, IQueryable<T>> includes = null,
             CancellationToken cancellationToken = default)
                {
                    IQueryable<T> query = _context.Set<T>();  // Changed from _dbSet to _context.Set<T>()

                    if (includes != null)
                    {
                        query = includes(query);
                    }

                    return await query
                        .AsNoTracking()
                        .FirstOrDefaultAsync(predicate, cancellationToken);
                }
    }
}
