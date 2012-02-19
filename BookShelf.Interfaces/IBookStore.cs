using System;
using System.Collections.Generic;
using BookShelf.Model.SearchParams;
using BookShelf.Model;

namespace BookShelf.Interfaces
{
    public interface IBookStore : IEntityStore<Book>
    {
        // Search is async
        void Search(BookSearchParams searchParams);
        event EventHandler<BookSearchEventArgs> SearchBooksCompleted;

        void Remove(int id);
        void RemoveMany(List<int> ids);
    }
}
