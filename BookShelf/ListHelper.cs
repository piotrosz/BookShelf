using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Data;

namespace BookShelf
{
    public static class ListHelper
    {
        public static void ListViewClick(RoutedEventArgs e, ICollectionView source, ResourceDictionary resources)
        {
            GridViewColumnHeader currentHeader = e.OriginalSource as GridViewColumnHeader;
            if (currentHeader != null && currentHeader.Role != GridViewColumnHeaderRole.Padding)
            {
                using (source.DeferRefresh())
                {
                    Func<SortDescription, bool> lamda = item => item.PropertyName.Equals(currentHeader.Column.Header.ToString());
                    if (source.SortDescriptions.Count(lamda) > 0)
                    {
                        SortDescription currentSortDescription = source.SortDescriptions.First(lamda);
                        ListSortDirection sortDescription = currentSortDescription.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;

                        currentHeader.Column.HeaderTemplate = currentSortDescription.Direction == ListSortDirection.Ascending ?
                            resources["HeaderTemplateArrowDown"] as DataTemplate : resources["HeaderTemplateArrowUp"] as DataTemplate;

                        source.SortDescriptions.Remove(currentSortDescription);
                        source.SortDescriptions.Insert(0, new SortDescription(currentHeader.Column.Header.ToString(), sortDescription));
                    }
                    else
                        source.SortDescriptions.Add(new SortDescription(currentHeader.Column.Header.ToString(), ListSortDirection.Ascending));
                }
            }
        }

        public static void FilterClick<T>(ICollectionView source, ComboBox cmbProperty, TextBox txtFilter)
            where T : class
        {
            source.Filter = item =>
            {
                var viewItem = item as T;
                if (viewItem == null) return false;

                PropertyInfo info = item.GetType().GetProperty(cmbProperty.Text);
                if (info == null) return false;

                return info.GetValue(viewItem, null).ToString().Contains(txtFilter.Text);
            };
        }

        public static void ClearClick(ICollectionView source)
        {
            source.Filter = item => true;
        }

        public static void GroupClick<T>(ICollectionView source, ComboBox cmbGroups)
            where T : class
        {
            source.GroupDescriptions.Clear();

            PropertyInfo pinfo = typeof(T).GetProperty(cmbGroups.Text);
            if (pinfo != null)
                source.GroupDescriptions.Add(new PropertyGroupDescription(pinfo.Name));
        }

        public static void ClearGrClick(ICollectionView source)
        {
            source.GroupDescriptions.Clear();
        }

        public static void NavigationClick(ICollectionView source, object sender)
        {
            Button button = sender as Button;

            switch (button.Tag.ToString())
            {
                case "0":
                    source.MoveCurrentToFirst();
                    break;
                case "1":
                    source.MoveCurrentToPrevious();
                    break;
                case "2":
                    source.MoveCurrentToNext();
                    break;
                case "3":
                    source.MoveCurrentToLast();
                    break;
            }
        }
    }
}
