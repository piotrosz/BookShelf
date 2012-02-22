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
    public class CategoryStore : Store<Category>, ICategoryStore
    {
        private IDocumentStore documentStore;

        public CategoryStore(IDocumentStore documentStore) : base(documentStore)
        {
            this.documentStore = documentStore;
        }

        public List<Category> Search(CategorySearchParams searchParams)
        {
            var result = new List<Category>();

            using (var session = documentStore.OpenSession())
            {
                IQueryable<Category> query = session.Query<Category>();

                if (searchParams != null)
                {
                    if (searchParams.IsDefined(searchParams.Name))
                        query = query.Where(c => c.Name.Contains(searchParams.Name));
                }
                result = query.ToList();
            }

            return result;
        }
    }
}
