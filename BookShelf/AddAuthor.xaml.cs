﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookShelf.Model;
using BookShelf.ViewModel;
using BookShelf.DataAccess;
#if MOCK
using BookShelf.DataAccess.Mock;
#else
using BookShelf.DataAccess.Raven;
#endif

namespace BookShelf
{
    public partial class AddAuthor : Window
    {
        public AddAuthor()
        {
            InitializeComponent();
            this.DataContext = new Author();
        }

        int noOfErrorsOnScreen = 0;

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                noOfErrorsOnScreen++;
            else
                noOfErrorsOnScreen--;
        }

        private void AddAuthor_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = noOfErrorsOnScreen == 0;
            e.Handled = true;
        }

        private void AddAuthor_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var item = (Author)(this.DataContext);
            StoreRepository.Author.Save(item);
            e.Handled = true;
            this.Close();
        }
    }
}
