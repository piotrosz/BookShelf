using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using BookShelf.Model;

#if MOCK
using BookShelf.DataAccess.Mock;
#else
using BookShelf.DataAccess.Raven;
#endif

namespace BookShelf.ViewModel
{
    public class AuthorsViewModel
    {
        private ObservableCollection<Author> authors;
        public ObservableCollection<Author> Authors
        {
            get
            {
                if (this.authors == null)
                    this.authors = new ObservableCollection<Author>(StoreRepository.Author.Search(null));
                return this.authors;
            }
        }

        public IEnumerable<string> ColumnNames
        {
            get { return typeof(Author).GetProperties().Select(p => p.Name); }
        }

        public void Refresh()
        {
            this.authors = new ObservableCollection<Author>(StoreRepository.Author.Search(null));
        }
    }
}
