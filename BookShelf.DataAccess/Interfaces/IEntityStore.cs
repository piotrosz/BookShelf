using System;
using System.Collections.Generic;

namespace BookShelf.DataAccess.Interfaces
{
    public interface IEntityStore<T>
    {
        void Save(T entity);
        T Get(int id);
        void Delete(T entity);
        List<T> GetMax();
    }
}
