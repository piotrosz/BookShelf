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
    public class LendingStore : Store<Lending>, ILendingStore
    {
        private IDocumentStore documentStore;

        public LendingStore(IDocumentStore documentStore) : base(documentStore)
        {
            this.documentStore = documentStore;
        }

        public List<Lending> Search(LendingSearchParams searchParams)
        {
            var result = new List<Lending>();

            using (var session = documentStore.OpenSession())
            {
                IQueryable<Lending> query = session.Query<Lending>();

                if (searchParams.IsDefined(searchParams.From))
                    query = query.Where(b => b.From >= searchParams.From);
                if (searchParams.IsDefined(searchParams.To))
                    query = query.Where(b => b.To <= searchParams.To);
                if (searchParams.IsDefined(searchParams.LenderFirstName))
                    query = query.Where(b => b.Person.FirstName.Contains(searchParams.LenderFirstName));
                if (searchParams.IsDefined(searchParams.LenderLastName))
                    query = query.Where(b => b.Person.LastName.Contains(searchParams.LenderLastName));

                result = query.ToList();
            }

            return result;
        }
    }
}
