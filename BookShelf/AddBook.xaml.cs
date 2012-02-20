using System;
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
using BookShelf.DataAccess.Raven;
using BookShelf.ViewModel;

namespace BookShelf
{
    public partial class AddBook : Window
    {
        private BookAddViewModel DataModel { get; set; }

        public AddBook()
        {
            this.DataModel = new BookAddViewModel();
            InitializeComponent();
        }

        public IEnumerable<Author> Authors
        {
            get { return this.DataModel.Authors; }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var item = (BookAddViewModel)(this.DataContext);
            StoreRepository.Book.Save(item.Book);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Book();
        }

        private void chooseImage_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Open file dialog
        }
    }
}
