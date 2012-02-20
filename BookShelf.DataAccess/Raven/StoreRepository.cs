using System;
using BookShelf.DataAccess.Interfaces;
using Raven.Client.Embedded;
using Raven.Client;

namespace BookShelf.DataAccess.Raven
{
    public static class StoreRepository
    {
        private static IDocumentStore documentStore = null;
        private static IDocumentStore DocumentStore
        {
            get
            {
                if (documentStore == null)
                {
                    documentStore = new EmbeddableDocumentStore { DataDirectory = "db" };
                    documentStore.Initialize();
                }

                return documentStore;
            }
        }

        private static IPersonStore _Person = null;
        public static IPersonStore Person
        {
            get
            {
                if (_Person == null)
                    _Person = new PersonStore(DocumentStore);
                return _Person;
            }
        }

        private static ICategoryStore _Category = null;
        public static ICategoryStore Category
        {
            get
            {
                if (_Category == null)
                    _Category = new CategoryStore(DocumentStore);
                return _Category;
            }
        }

        private static IBookStore _Book = null;
        public static IBookStore Book
        {
            get
            {
                if (_Book == null)
                    _Book = new BookStore(DocumentStore);
                return _Book;
            }
        }

        private static IAuthorStore _Author = null;
        public static IAuthorStore Author
        {
            get
            {
                if (_Author == null)
                    _Author = new AuthorStore(DocumentStore);
                return _Author;
            }
        }

        private static ILendingStore _Lending = null;
        public static ILendingStore Lending
        {
            get
            {
                if (_Lending == null)
                    _Lending = new LendingStore(DocumentStore);
                return _Lending;
            }
        }

        private static IPublisherStore _Publisher = null;
        public static IPublisherStore Publisher
        {
            get
            {
                if (_Publisher == null)
                    _Publisher = new PublisherStore(DocumentStore);
                return _Publisher;
            }
        }
    }
}
