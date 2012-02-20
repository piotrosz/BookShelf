using System;
using BookShelf.DataAccess.Interfaces;

namespace BookShelf.DataAccess.Mock
{
    public class StoreRepository
    {
        private static IBookStore book = null;
        public static IBookStore Book
        {
            get
            {
                if (book == null)
                    book = new BookStore();
                return book;
            }
        }

        private static IAuthorStore author = null;
        public static IAuthorStore Author
        {
            get
            {
                if (author == null)
                    author = new AuthorStore();
                return author;
            }
        }
    }
}
