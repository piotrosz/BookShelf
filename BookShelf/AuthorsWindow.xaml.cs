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
using BookShelf.ViewModel;
using System.ComponentModel;
using BookShelf.Model;
using System.Reflection;

namespace BookShelf
{
    public partial class AuthorsWindow : Window
    {
        private AuthorsViewModel DataModel { get; set; }
        private ICollectionView Source { get; set; }

        public AuthorsWindow()
        {
            InitializeComponent();

            this.DataModel = new AuthorsViewModel();

            this.Source = CollectionViewSource.GetDefaultView(this.DataModel.Authors);
            this.grdMain.DataContext = this.DataModel;
            this.lvItems.DataContext = this.Source;
        }

        private void ListView_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.ListViewClick(e, this.Source, this.Resources);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.FilterClick<Author>(this.Source, cmbProperty, txtFilter);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.ClearClick(this.Source);
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.GroupClick<Author>(this.Source, cmbGroups);
        }

        private void btnClearGr_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.ClearGrClick(this.Source);
        }

        private void btnNavigation_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.NavigationClick(this.Source, sender);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddAuthor();
            addWindow.Owner = this;
            addWindow.Closed += new EventHandler(addWindow_Closed);
            addWindow.Show();
        }

        void addWindow_Closed(object sender, EventArgs e)
        {
            DataModel.Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
