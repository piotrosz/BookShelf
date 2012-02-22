using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BookShelf.Model;
#if MOCK
using BookShelf.DataAccess.Mock;
#else
using BookShelf.DataAccess.Raven;
#endif

namespace BookShelf.ViewModel
{
    public class BooksViewModel
    {
        private ObservableCollection<Book> books;
        public ObservableCollection<Book> Books
        {
            get
            {
                if (this.books == null)
                    this.books = new ObservableCollection<Book>(StoreRepository.Book.Search(null));
                return this.books;
            }
        }

        public IEnumerable<string> ColumnNames
        {
            get { return typeof(Book).GetProperties().Select(p => p.Name); }
        }

        public IEnumerable<Author> Authors
        {
            get { return StoreRepository.Author.Search(null); }
        }

        public void Refresh()
        {
            this.books = new ObservableCollection<Book>(StoreRepository.Book.Search(null));
        }
    }
}
