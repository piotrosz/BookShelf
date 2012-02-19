using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using BookShelf.Model.SearchParams;
using BookShelf.DataAccess.Interfaces;

namespace BookShelf.DataAccess.Raven
{
    public class BookStore : Store<Book>, IBookStore
    {
        public List<Book> Search(BookSearchParams searchParams)
        {
            var result = new List<Book>();

            using (var session = DocumentStore.OpenSession())
            {
                IQueryable<Book> query = session.Query<Book>();

                if (searchParams.IsDefined(searchParams.Title))
                {
                    query = query.Where(b => b.Title.Contains(searchParams.Title));
                }

                result = query
                    .OrderBy(b => b.Title)
                    .ToList();

                return result;
            }
        }

        public void Remove(int id)
        {
            using (var session = DocumentStore.OpenSession())
            {
                session.Advanced.DatabaseCommands.Delete("Books/" + id, null);
            }
        }

        public void RemoveMany(List<int> ids)
        {
            using (var session = DocumentStore.OpenSession())
            {
                foreach (var item in ids)
                {
                    session.Advanced.DatabaseCommands.Delete("Books/" + item, null);
                }
            }
        }
    }
}
