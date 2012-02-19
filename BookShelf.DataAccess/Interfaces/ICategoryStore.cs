using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using BookShelf.Model.SearchParams;

namespace BookShelf.DataAccess.Interfaces
{
    public interface ICategoryStore : IEntityStore<Category>
    {
        List<Category> Search(CategorySearchParams searchParams);
    }
}
