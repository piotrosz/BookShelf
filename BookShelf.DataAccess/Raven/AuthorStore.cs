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
    public class AuthorStore : Store<Author>, IAuthorStore
    {
        private IDocumentStore documentStore;

        public AuthorStore(IDocumentStore documentStore) : base(documentStore)
        {
            this.documentStore = documentStore;
        }

        public List<Author> Search(PersonSearchParams searchParams)
        {
            var result = new List<Author>();

            using (var session = documentStore.OpenSession())
            {
                IQueryable<Author> query = session.Query<Author>();

                if (searchParams != null)
                {
                    if (searchParams.IsDefined(searchParams.FirstName))
                        query = query.Where(a => a.FirstName == searchParams.FirstName);
                    if (searchParams.IsDefined(searchParams.LastName))
                        query = query.Where(a => a.LastName == searchParams.LastName);
                }

                result = query.ToList();
            }

            return result;
        }
    }
}
