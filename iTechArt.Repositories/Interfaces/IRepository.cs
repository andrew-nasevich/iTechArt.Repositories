using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iTechArt.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<T> GetByIdAsync(object id);

        Task<IReadOnlyCollection<T>> WhereAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Remove(T entity);

        void Update(T entity);
    }
}