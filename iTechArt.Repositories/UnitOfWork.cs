using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using iTechArt.Repositories.Interfaces;

namespace iTechArt.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly DbContext Context;

        private readonly IDictionary<Type, object> _repositories;

        private bool _disposed;


        public UnitOfWork(DbContext dbContext)
        {
            Context = dbContext;
            _repositories = new Dictionary<Type, object>();
        }


        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out var repository))
            {
                return (IRepository<T>)repository;
            }

            repository = CreateRepository<T>();
            _repositories.Add(typeof(T), repository);

            return (IRepository<T>)repository;
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~UnitOfWork()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if(disposing)
            {
                Context.Dispose();
            }
            _disposed = true;
        }

        protected virtual IRepository<T> CreateRepository<T>() where T : class
        {
            return new Repository<T>(Context);
        }
    }
}