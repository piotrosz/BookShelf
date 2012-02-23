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

        public IEnumerable<Tuple<string, string>> ColumnNames
        {
            get
            {
                return new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("Title", "Title"),
                    new Tuple<string, string>("Author", "Author"),
                    new Tuple<string, string>("CategoriesCsv", "Category")
                };
                //return typeof(Book).GetProperties().Select(p => p.Name); 
            }
        }

        public IEnumerable<Author> Authors
        {
            get { return StoreRepository.Author.Search(null); }
        }

        public IEnumerable<Category> Categories
        {
            get { return StoreRepository.Category.Search(null); }
        }

        public void Refresh()
        {
            this.books = new ObservableCollection<Book>(StoreRepository.Book.Search(null));
        }
    }
}
