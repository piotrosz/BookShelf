using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShelf.DataAccess.Interfaces;
using BookShelf.Model;
using BookShelf.Model.SearchParams;

namespace BookShelf.DataAccess.Mock
{
    public class CategoryStore : ICategoryStore
    {
        #region static list
        private static List<Category> list = new List<Category>()
        {
            new Category { Id = 1, Name = "History" },
            new Category { Id = 2, Name = "Culture" },
            new Category { Id = 3, Name = "Russia" },
            new Category { Id = 4, Name = "Non fiction" },
            new Category { Id = 5, Name = "Biography" },
            new Category { Id = 6, Name = "Novel" }
        }; 
        #endregion

        public List<Category> Search(CategorySearchParams searchParams)
        {
            return list;
        }

        public void Save(Category entity)
        {
            if (Get(entity.Id) == null)
                list.Add(entity);
            else
            {
                var toUpdate = Get(entity.Id);
                toUpdate = entity;
            }
        }

        public Category Get(int id)
        {
            return list.SingleOrDefault(x => x.Id == id);
        }

        public void Delete(IEntity entity)
        {
            list.Remove(entity as Category);
        }

        public List<Category> GetMax()
        {
            return list;
        }
    }
}
