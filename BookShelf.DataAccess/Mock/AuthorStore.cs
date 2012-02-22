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
            if (Get(entity.Id) == null)
                list.Add(entity);
            else
            {
                var toUpdate = Get(entity.Id);
                toUpdate = entity;
            }
        }

        public Author Get(int id)
        {
            return list.SingleOrDefault(x => x.Id == id);
        }

        public void Delete(Author entity)
        {
            list.Remove(entity);
        }

        public List<Author> GetMax()
        {
            return list;
        }
    }
}
