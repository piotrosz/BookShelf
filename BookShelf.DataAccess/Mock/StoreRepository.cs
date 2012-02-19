using System;
using BookShelf.DataAccess.Interfaces;

namespace BookShelf.DataAccess.Mock
{
    public class StoreRepository
    {
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
    }
}
