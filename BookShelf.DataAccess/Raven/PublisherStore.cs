using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using BookShelf.DataAccess.Interfaces;
using Raven.Client;

namespace BookShelf.DataAccess.Raven
{
    public class PublisherStore : Store<Publisher>, IPublisherStore
    {
        private IDocumentStore documentStore;

        public PublisherStore(IDocumentStore documentStore) : base(documentStore)
        {
            this.documentStore = documentStore;
        }
    }
}
