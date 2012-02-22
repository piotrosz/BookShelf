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
using BookShelf.Model;
using BookShelf.ViewModel;
#if MOCK
using BookShelf.DataAccess.Mock;
#else
using BookShelf.DataAccess.Raven;
#endif

namespace BookShelf
{
    public partial class CategoriesWindow : Window
    {
        private CategoriesViewModel DataModel { get; set; }
        private ICollectionView Source { get; set; }

        public CategoriesWindow()
        {
            this.DataModel = new CategoriesViewModel();
            InitializeComponent();
            DataBind();
        }

        private void DataBind()
        {
            this.Source = CollectionViewSource.GetDefaultView(this.DataModel.Categories);
            this.grdMain.DataContext = this.DataModel;
            this.lvItems.DataContext = this.Source;
        }

        #region Grouping and filtering
        private void ListView_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.ListViewClick(e, this.Source, this.Resources);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.FilterClick<Category>(this.Source, cmbProperty, txtFilter);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.ClearClick(this.Source);
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            ListHelper.GroupClick<Category>(this.Source, cmbGroups);
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
            var addWindow = new AddCategory();
            addWindow.Owner = this;
            addWindow.Closed += new EventHandler(addWindow_Closed);
            addWindow.Show();
        }

        void addWindow_Closed(object sender, EventArgs e)
        {
            DataModel.Refresh();
            DataBind();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvItems.SelectedItem != null)
            {
                var category = (Category)lvItems.SelectedItem;
                StoreRepository.Category.Delete(category);
                DataModel.Refresh();
                DataBind();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvItems.SelectedItem != null)
            {
                var category = (Category)lvItems.SelectedItem;
                StoreRepository.Category.Save(category);
                DataModel.Refresh();
                DataBind();
            }
        }
        #endregion
    }
}
