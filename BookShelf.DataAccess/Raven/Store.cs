using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using Raven.Client.Embedded;
using BookShelf.Model;

namespace BookShelf.DataAccess.Raven
{
    public class Store<T>
    {
        private const int MAX_ITEMS = 1000;

        private IDocumentStore documentStore;

        public Store(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public void Save(T entity)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(entity);
                session.SaveChanges();
            }
        }

        public T Get(int id)
        {
            T result = default(T);

            using (var session = documentStore.OpenSession())
            {
                result = session.Load<T>(id);
            }

            return result;
        }

        public void Delete(IEntity entity) 
        {
            using (var session = documentStore.OpenSession())
            {
                T toDelete = session.Load<T>(entity.Id);
                session.Delete<T>(toDelete);
                session.SaveChanges();
            }
        }

        public List<T> GetMax()
        {
            var result = new List<T>();

            using (var session = documentStore.OpenSession())
            {
                result = session.Query<T>().Take(MAX_ITEMS).ToList();
            }

            return result;
        }
    }
}
