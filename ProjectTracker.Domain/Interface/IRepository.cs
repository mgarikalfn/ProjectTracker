using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Domain.Entities;

namespace ProjectTracker.Domain.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> ExistsAsync(string id);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
