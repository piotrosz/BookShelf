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
    public class CategoriesViewModel
    {
        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get
            {
                if(this.categories == null)
                    this.categories = new ObservableCollection<Category>(StoreRepository.Category.Search(null));
                return this.categories;
            }
        }

        public IEnumerable<string> ColumnNames
        {
            get { return typeof(Category).GetProperties().Select(p => p.Name); }
        }

        public void Refresh()
        {
            this.categories = new ObservableCollection<Category>(StoreRepository.Category.Search(null));
        }
    }
}
