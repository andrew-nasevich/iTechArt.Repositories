using iTechArt.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iTechArt.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _сontext;
        private readonly DbSet<T> _entities;


        public Repository(DbContext сontext) 
        {
            _сontext = сontext;
            _entities = _сontext.Set<T>();
        }


        public async Task<IReadOnlyCollection<T>> GetAllAsync() 
        {
            return await GetAllQuery().ToListAsync();
        }

        public async virtual Task<T> GetByIdAsync(object id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<T>> WhereAsync(Expression<Func<T, bool>> predecate)
        {
            return await GetAllQuery().Where(predecate).ToListAsync();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Remove(T entity)
        {
            _entities.Attach(entity);
            _entities.Remove(entity);
        }

        public void Update(T entity)
        {
            _сontext.Entry(entity).State = EntityState.Modified;
        }


        protected virtual IQueryable<T> GetAllQuery()
        {
            return _entities;
        }

        protected IQueryable<T> GetQuery(params Expression<Func<T, object>>[] includes)
        { 
            return includes.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(_entities, (current, include) => current.Include(include));
        }
    }
}