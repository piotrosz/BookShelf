using System;
using System.Collections.Generic;
using BookShelf.Model;

namespace BookShelf.DataAccess.Interfaces
{
    public interface IEntityStore<T>
    {
        void Save(T entity);
        T Get(int id);
        void Delete(IEntity entity);
        List<T> GetMax();
    }
}
