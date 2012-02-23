using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using BookShelf.Model.SearchParams;
using BookShelf.DataAccess.Interfaces;
using Raven.Client;

namespace BookShelf.DataAccess.Raven
{
    public class BookStore : Store<Book>, IBookStore
    {
        private IDocumentStore documentStore;

        public BookStore(IDocumentStore documentStore) : base(documentStore)
        {
            this.documentStore = documentStore;
        }

        public List<Book> Search(BookSearchParams searchParams)
        {
            var result = new List<Book>();

            using (var session = documentStore.OpenSession())
            {
                IQueryable<Book> query = session.Query<Book>();

                if (searchParams != null)
                {
                    if (searchParams.IsDefined(searchParams.Title))
                    {
                        query = query.Where(b => b.Title == searchParams.Title);
                    }
                }

                result = query
                    .OrderBy(b => b.Title)
                    .ToList();

                return result;
            }
        }

        public void Remove(int id)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Advanced.DatabaseCommands.Delete("Books/" + id, null);
            }
        }

        public void RemoveMany(List<int> ids)
        {
            using (var session = documentStore.OpenSession())
            {
                foreach (var item in ids)
                {
                    session.Advanced.DatabaseCommands.Delete("Books/" + item, null);
                }
            }
        }
    }
}
