using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using BookShelf.DataAccess.Interfaces;

namespace BookShelf.DataAccess.Raven
{
    public class PersonStore : Store<Person>, IPersonStore
    {

    }
}
