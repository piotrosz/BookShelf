using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.Model;
using BookShelf.Model.SearchParams;
using BookShelf.DataAccess.Interfaces;

namespace BookShelf.DataAccess.Mock
{
    public class BookStore : IBookStore
    {
        private static List<Book> list = new List<Book>()
        {
            new Book
            { 
                Id = 1,
                Author = new Author 
                { 
                    Id = 1, 
                    FirstName = "Orlando",
                    LastName = "Figes"
                },
                Categories = new List<Category>()
                {
                    new Category { Id = 1, Name = "History" },
                    new Category { Id = 2, Name = "Culture" },
                    new Category { Id = 3, Name = "Russia" },
                    new Category { Id = 4, Name = "Non fiction" }
                },
                Description = "The cultural history of Russia",
                NumberOfPages = 678, 
                Title = "Natasha's dance",
                Year = 2001
            },
            new Book
            {
                Id = 2,
                Author = new Author
                {
                    Id = 2,
                    FirstName = "Tadeusz",
                    LastName = "Konwicki"
                },
                Categories = new List<Category>()
                {
                    new Category { Id = 5, Name = "Biography" },
                    new Category { Id = 4, Name = "Non fiction" }
                },
                Description = "Przemysław Kaniecki interviews Tadeusz Konwicki",
                NumberOfPages = 333,
                Title = "W pośpiechu",
                Year = 2011
            },
            new Book
            {
                Id = 3,
                Author = new Author
                {
                    Id = 3,
                    FirstName = "Wiktor",
                    LastName = "Pielewin"
                },
                Categories = new List<Category>()
                {
                    new Category { Id = 6, Name = "Novel" }
                },
                NumberOfPages = 234,
                Title = "Generation P",
                Year = 2004
            },
            new Book
            {
                Id = 4,
                Author = new Author
                {
                    Id = 3,
                    FirstName = "Wiktor",
                    LastName = "Pielewin"
                },
                Categories = new List<Category>()
                {
                    new Category { Id = 6, Name = "Novel" }
                },
                NumberOfPages = 405,
                Title = "T",
                TitleOriginal = "T",
                Year = 2012
            }
        };

        public List<Book> Search(BookSearchParams searchParams)
        {
            var result = list;

            if (searchParams != null)
            {
                if (searchParams.IsDefined(searchParams.Title))
                    result = result.Where(x => x.Title.Contains(searchParams.Title)).ToList();
            }

            return result;
        }

        public void Remove(int id)
        {
            list.Remove(Get(id));
        }

        public void RemoveMany(List<int> ids)
        {
            foreach (var item in ids)
            {
                list.Remove(Get(item));
            }
        }

        public void Save(Book entity)
        {
            if (Get(entity.Id) == null)
            {
                list.Add(entity);
            }
            else
            {
                var toUpdate = Get(entity.Id);
                toUpdate = entity;
            }
        }

        public Book Get(int id)
        {
            return list.SingleOrDefault(x => x.Id == id);
        }

        public void Delete(Book entity)
        {
            list.Remove(entity);
        }

        public List<Book> GetMax()
        {
            return list;
        }
    }
}
