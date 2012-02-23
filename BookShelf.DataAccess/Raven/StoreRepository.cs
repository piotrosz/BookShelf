using System;
using System.Collections.Generic;
using Raven.Client.Embedded;
using Raven.Client;
using BookShelf.Model;
using BookShelf.Model.SearchParams;
using BookShelf.DataAccess.Interfaces;
using System.IO;

namespace BookShelf.DataAccess.Raven
{
    public static class StoreRepository
    {
        private static IDocumentStore documentStore = null;
        private static IDocumentStore DocumentStore
        {
            get
            {
                if (documentStore == null)
                {
                    documentStore = new EmbeddableDocumentStore { DataDirectory = "db" };
                    documentStore.Initialize();
                }

                return documentStore;
            }
        }

        private static IPersonStore person = null;
        public static IPersonStore Person
        {
            get
            {
                if (person == null)
                    person = new PersonStore(DocumentStore);
                return person;
            }
        }

        private static ICategoryStore category = null;
        public static ICategoryStore Category
        {
            get
            {
                if (category == null)
                    category = new CategoryStore(DocumentStore);
                return category;
            }
        }

        private static IBookStore book = null;
        public static IBookStore Book
        {
            get
            {
                if (book == null)
                    book = new BookStore(DocumentStore);
                return book;
            }
        }

        private static IAuthorStore author = null;
        public static IAuthorStore Author
        {
            get
            {
                if (author == null)
                    author = new AuthorStore(DocumentStore);
                return author;
            }
        }

        private static ILendingStore lending = null;
        public static ILendingStore Lending
        {
            get
            {
                if (lending == null)
                    lending = new LendingStore(DocumentStore);
                return lending;
            }
        }

        private static IPublisherStore publisher = null;
        public static IPublisherStore Publisher
        {
            get
            {
                if (publisher == null)
                    publisher = new PublisherStore(DocumentStore);
                return publisher;
            }
        }

        public static void InsertInitialData()
        {
            Category.Save(new Category { Name = "Novel" });
            Category.Save(new Category { Name = "Non fiction" });
            Category.Save(new Category { Name = "Biography" });
            Category.Save(new Category { Name = "Russia" });
            Category.Save(new Category { Name = "History" });

            Author.Save(new Author { FirstName = "Orlando", LastName = "Figes" });
            Author.Save(new Author { FirstName = "Wiktor", LastName = "Pielewin" });
            Author.Save(new Author { FirstName = "Lew", LastName = "Tołstoj" });
            Author.Save(new Author { FirstName = "Tadeusz", LastName = "Konwicki" });
            Author.Save(new Author { FirstName = "Miron", LastName = "Białoszewski" });

            Publisher.Save(new Publisher { Name = "WAB" });
            Publisher.Save(new Publisher { Name = "Czarne" });
            Publisher.Save(new Publisher { Name = "Zak" });
            Publisher.Save(new Publisher { Name = "WL" });

            Book.Save(new Book
            {
                Author = Author.Search(new PersonSearchParams { LastName = "Pielewin" })[0],
                Categories = new List<Category>
                {
                    Category.Search(new CategorySearchParams { Name = "Novel" })[0]
                },
                Description = "postmodernistic pulp",
                NumberOfPages = 406,
                Title = "T",
                TitleOriginal = "T",
                Year = 2012,
                Image = File.ReadAllBytes(@"H:\img1.jpg")
            });
            Book.Save(new Book
            {
                Author = Author.Search(new PersonSearchParams { LastName = "Figes" })[0],
                Categories = new List<Category>
                {
                    Category.Search(new CategorySearchParams { Name = "Russia" })[0],
                    Category.Search(new CategorySearchParams { Name = "History" })[0]
                },
                Title = "Taniec Nataszy",
                Year = 2011,
                Description = "Cultural Russia history, XIX-XX",
                Image = File.ReadAllBytes(@"H:\img2.jpg")
            });
            Book.Save(new Book
            {
                Author = Author.Search(new PersonSearchParams { LastName = "Konwicki" })[0],
                Categories = new List<Category>
                {
                    Category.Search(new CategorySearchParams { Name = "Non fiction" })[0]
                },
                Title = "W pośpiechu",
                Year = 2011,
                Image = File.ReadAllBytes(@"H:\img3.jpg")
            });
        }
    }
}
