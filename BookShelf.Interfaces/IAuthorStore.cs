using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using BookShelf.Model.SearchParams;

namespace BookShelf.Interfaces
{
    public interface IAuthorStore : IEntityStore<Author>
    {
        List<Author> Search(PersonSearchParams searchParams);
    }
}
