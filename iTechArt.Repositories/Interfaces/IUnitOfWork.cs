﻿using System.Threading.Tasks;

namespace iTechArt.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        Task SaveAsync();
    }
}