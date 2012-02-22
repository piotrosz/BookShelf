using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using System.Collections.ObjectModel;
#if MOCK
using BookShelf.DataAccess.Mock;
#else
using BookShelf.DataAccess.Raven;
#endif

namespace BookShelf.ViewModel
{
    public class BookAddViewModel
    {
        private ObservableCollection<Author> authors;
        public ObservableCollection<Author> Authors
        {
            get
            {
                if (this.authors == null)
                    this.authors = new ObservableCollection<Author>(StoreRepository.Author.GetMax());
                return this.authors;
            }
        }

        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get
            {
                if (this.categories == null)
                    this.categories = new ObservableCollection<Category>(StoreRepository.Category.GetMax());
                return this.categories;
            }
        }

        public Book Book = new Book();
    }
}
