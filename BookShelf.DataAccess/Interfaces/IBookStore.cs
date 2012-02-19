using System;
using System.Collections.Generic;
using BookShelf.Model.SearchParams;
using BookShelf.Model;

namespace BookShelf.DataAccess.Interfaces
{
    public interface IBookStore : IEntityStore<Book>
    {
        List<Book> Search(BookSearchParams searchParams);
        void Remove(int id);
        void RemoveMany(List<int> ids);
    }
}
