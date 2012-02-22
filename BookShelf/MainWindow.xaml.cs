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
using System.ComponentModel;
using System.Reflection;
using BookShelf.ViewModel;
using BookShelf.Model;
#if MOCK
using BookShelf.DataAccess.Mock;
#else
using BookShelf.DataAccess.Raven;
#endif

namespace BookShelf
{
    public partial class MainWindow : Window
    {
        private BooksViewModel DataModel { get; set; }
        private ICollectionView Source { get; set; }

        public IEnumerable<Author> Authors
        {
            get { return this.DataModel.Authors; }
        }

        public MainWindow()
        {
            this.DataModel = new BooksViewModel();
            InitializeComponent();
            DataBind();
        }

        private void DataBind()
        {
            this.Source = CollectionViewSource.GetDefaultView(this.DataModel.Books);
            this.grdMain.DataContext = this.DataModel;
            this.lvItems.DataContext = this.Source;
        }

        #region Grouping & filtering
        private void ListView_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.ListViewClick(e, this.Source, this.Resources);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.FilterClick<Book>(this.Source, cmbProperty, txtFilter);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.ClearClick(this.Source);
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.GroupClick<Book>(this.Source, cmbGroups);
        }

        private void btnClearGr_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.ClearGrClick(this.Source);
        }

        private void btnNavigation_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.NavigationClick(this.Source, sender);
        }
        #endregion

        #region Elements manipulation
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addBookWindow = new AddBook();
            addBookWindow.Owner = this;
            addBookWindow.Closed += new EventHandler(addBookWindow_Closed);
            addBookWindow.Show();
        }

        void addBookWindow_Closed(object sender, EventArgs e)
        {
            DataModel.Refresh();
            DataBind();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvItems.SelectedItem != null)
            {
                var book = (Book)lvItems.SelectedItem;
                StoreRepository.Book.Delete(book);
                DataModel.Refresh();
                DataBind();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvItems.SelectedItem != null)
            {
                var book = (Book)lvItems.SelectedItem;
                StoreRepository.Book.Save(book);
                DataModel.Refresh();
                DataBind();
            }
        }
        #endregion

        #region Menu clicks
        private void authors_Click(object sender, RoutedEventArgs e)
        {
            var authorsWindow = new AuthorsWindow();
            authorsWindow.Owner = this;
            authorsWindow.Closed += new EventHandler(authorsWindow_Closed);
            authorsWindow.Show();
        }

        void authorsWindow_Closed(object sender, EventArgs e)
        {
            DataModel.Refresh();
            DataBind();
        }

        private void categories_Click(object sender, RoutedEventArgs e)
        {
            var categoriesWindow = new CategoriesWindow();
            categoriesWindow.Owner = this;
            categoriesWindow.Closed += new EventHandler(categoriesWindow_Closed);
            categoriesWindow.Show();
        }

        void categoriesWindow_Closed(object sender, EventArgs e)
        {
            DataModel.Refresh();
            DataBind();
        }

        private void newLending_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lendings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void borrowers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void about_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
