using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using System.Collections.ObjectModel;
using BookShelf.DataAccess.Raven;

namespace BookShelf.ViewModel
{
    public class BookAddViewModel
    {
        private ObservableCollection<Author> authors;
        public ObservableCollection<Author> Authors
        {
            get
            {
                this.authors = this.authors ?? new ObservableCollection<Author>(StoreRepository.Author.Search(null));
                return this.authors;
            }
        }
    }
}
