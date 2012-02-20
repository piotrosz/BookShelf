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
            InitializeComponent();
            this.DataModel = new BooksViewModel();
            DataBind();
        }

        private void DataBind()
        {
            this.Source = CollectionViewSource.GetDefaultView(this.DataModel.Books);
            this.grdMain.DataContext = this.DataModel;
            this.lvItems.DataContext = this.Source;
        }

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

        #region Elements manipulation
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addBookWindow = new AddBook();
            addBookWindow.Owner = this;
            addBookWindow.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvItems.SelectedItem != null)
            {
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

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

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
