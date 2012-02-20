using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using BookShelf.DataAccess.Interfaces;
using Raven.Client;

namespace BookShelf.DataAccess.Raven
{
    public class PersonStore : Store<Person>, IPersonStore
    {
        private IDocumentStore documentStore;

        public PersonStore(IDocumentStore documentStore) : base(documentStore)
        {
            this.documentStore = documentStore;
        }
    }
}
