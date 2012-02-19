﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BookShelf.Model;
using BookShelf.DataAccess.Raven;

namespace BookShelf.ViewModel
{
    public class BooksViewModel
    {
        private ObservableCollection<Book> books;
        public ObservableCollection<Book> Books
        {
            get
            {
                this.books = this.books ?? new ObservableCollection<Book>(StoreRepository.Book.GetMax());
                return this.books;
            }
        }

        public IEnumerable<string> ColumnNames
        {
            get { return typeof(Book).GetProperties().Select(p => p.Name); }
        }

        public IEnumerable<Author> Authors
        {
            get { return StoreRepository.Author.GetMax(); }
        }
    }
}
