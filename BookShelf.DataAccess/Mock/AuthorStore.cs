using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.DataAccess.Interfaces;
using BookShelf.Model;
using BookShelf.Model.SearchParams;

namespace BookShelf.DataAccess.Mock
{
    public class AuthorStore : IAuthorStore
    {
        private static List<Author> list = new List<Author>()
        {
            new Author 
            { 
                Id = 1, 
                FirstName = "Orlando",
                LastName = "Figes"
            },
            new Author
            {
                Id = 2,
                FirstName = "Tadeusz",
                LastName = "Konwicki"
            },
            new Author
            {
                Id = 3,
                FirstName = "Wiktor",
                LastName = "Pielewin"
            }
        };

        public List<Author> Search(PersonSearchParams searchParams)
        {
            return list;
        }

        public void Save(Author entity)
        {
            throw new NotImplementedException();
        }

        public Author Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Author entity)
        {
            throw new NotImplementedException();
        }

        public List<Author> GetMax()
        {
            return list;
        }
    }
}
