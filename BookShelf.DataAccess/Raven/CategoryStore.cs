using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using BookShelf.Model.SearchParams;
using BookShelf.DataAccess.Interfaces;

namespace BookShelf.DataAccess.Raven
{
    public class CategoryStore : Store<Category>, ICategoryStore
    {
        public List<Category> Search(CategorySearchParams searchParams)
        {
            var result = new List<Category>();

            using (var session = DocumentStore.OpenSession())
            {
                IQueryable<Category> query = session.Query<Category>();

                if (searchParams.IsDefined(searchParams.Name))
                    query = query.Where(c => c.Name.Contains(searchParams.Name));

                result = query.ToList();
            }

            return result;
        }
    }
}
