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

            this.Source = CollectionViewSource.GetDefaultView(this.DataModel.Books);
            this.grdMain.DataContext = this.DataModel;
            this.lvItems.DataContext = this.Source;
        }

        private void ListView_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader currentHeader = e.OriginalSource as GridViewColumnHeader;
            if (currentHeader != null && currentHeader.Role != GridViewColumnHeaderRole.Padding)
            {
                using (this.Source.DeferRefresh())
                {
                    Func<SortDescription, bool> lamda = item => item.PropertyName.Equals(currentHeader.Column.Header.ToString());
                    if (this.Source.SortDescriptions.Count(lamda) > 0)
                    {
                        SortDescription currentSortDescription = this.Source.SortDescriptions.First(lamda);
                        ListSortDirection sortDescription = currentSortDescription.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;


                        currentHeader.Column.HeaderTemplate = currentSortDescription.Direction == ListSortDirection.Ascending ?
                            this.Resources["HeaderTemplateArrowDown"] as DataTemplate : this.Resources["HeaderTemplateArrowUp"] as DataTemplate;

                        this.Source.SortDescriptions.Remove(currentSortDescription);
                        this.Source.SortDescriptions.Insert(0, new SortDescription(currentHeader.Column.Header.ToString(), sortDescription));
                    }
                    else
                        this.Source.SortDescriptions.Add(new SortDescription(currentHeader.Column.Header.ToString(), ListSortDirection.Ascending));
                }


            }


        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            this.Source.Filter = item =>
            {
                var viewItem = item as Book;
                if (viewItem == null) return false;

                PropertyInfo info = item.GetType().GetProperty(cmbProperty.Text);
                if (info == null) return false;

                return info.GetValue(viewItem, null).ToString().Contains(txtFilter.Text);

            };
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.Source.Filter = item => true;
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            this.Source.GroupDescriptions.Clear();

            PropertyInfo pinfo = typeof(Book).GetProperty(cmbGroups.Text);
            if (pinfo != null)
                this.Source.GroupDescriptions.Add(new PropertyGroupDescription(pinfo.Name));

        }

        private void btnClearGr_Click(object sender, RoutedEventArgs e)
        {
            this.Source.GroupDescriptions.Clear();
        }

        private void btnNavigation_Click(object sender, RoutedEventArgs e)
        {
            Button CurrentButton = sender as Button;

            switch (CurrentButton.Tag.ToString())
            {
                case "0":
                    this.Source.MoveCurrentToFirst();
                    break;
                case "1":
                    this.Source.MoveCurrentToPrevious();
                    break;
                case "2":
                    this.Source.MoveCurrentToNext();
                    break;
                case "3":
                    this.Source.MoveCurrentToLast();
                    break;
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addBookWindow = new AddBook();
            addBookWindow.Owner = this;
            addBookWindow.Show();
        }

        private void authors_Click(object sender, RoutedEventArgs e)
        {
            var authorsWindow = new AuthorsWindow();
            authorsWindow.Owner = this;
            authorsWindow.Show();
        }
    }
}
