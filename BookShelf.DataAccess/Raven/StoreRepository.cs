using System;
using BookShelf.DataAccess.Interfaces;

namespace BookShelf.DataAccess.Raven
{
    public static class StoreRepository
    {
        private static IPersonStore _Person = null;
        public static IPersonStore Person
        {
            get
            {
                if (_Person == null)
                    _Person = new PersonStore();
                return _Person;
            }
        }

        private static ICategoryStore _Category = null;
        public static ICategoryStore Category
        {
            get
            {
                if (_Category == null)
                    _Category = new CategoryStore();
                return _Category;
            }
        }

        private static IBookStore _Book = null;
        public static IBookStore Book
        {
            get
            {
                if (_Book == null)
                    _Book = new BookStore();
                return _Book;
            }
        }

        private static IAuthorStore _Author = null;
        public static IAuthorStore Author
        {
            get
            {
                if (_Author == null)
                    _Author = new AuthorStore();
                return _Author;
            }
        }

        private static ILendingStore _Lending = null;
        public static ILendingStore Lending
        {
            get
            {
                if (_Lending == null)
                    _Lending = new LendingStore();
                return _Lending;
            }
        }

        private static IPublisherStore _Publisher = null;
        public static IPublisherStore Publisher
        {
            get
            {
                if (_Publisher == null)
                    _Publisher = new PublisherStore();
                return _Publisher;
            }
        }
    }
}
