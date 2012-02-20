using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using BookShelf.Model;
using BookShelf.DataAccess.Raven;

namespace BookShelf.ViewModel
{
    public class AuthorsViewModel
    {
        public IEnumerable<string> ColumnNames
        {
            get { return typeof(Author).GetProperties().Select(p => p.Name); }
        }

        private ObservableCollection<Author> authors;
        public ObservableCollection<Author> Authors
        {
            get
            {
                this.authors = this.authors ?? new ObservableCollection<Author>(StoreRepository.Author.Search(null));
                return this.authors;
            }
        }

        public void Refresh()
        {
            this.authors = new ObservableCollection<Author>(StoreRepository.Author.Search(null));
        }
    }
}
