using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using System.Drawing;
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

        public IEnumerable<Category> Categories
        {
            get { return this.DataModel.Categories; }
        }

        public MainWindow()
        {
            this.DataModel = new BooksViewModel();

            if (this.DataModel.Books == null || this.DataModel.Books.Count == 0)
                StoreRepository.InsertInitialData();

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
            addBookWindow.Closing += new CancelEventHandler(addBookWindow_Closing);
            addBookWindow.ShowDialog();
        }

        void addBookWindow_Closing(object sender, CancelEventArgs e)
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
            MessageBox.Show("Not implemented");
        }

        private void lendings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented");
        }

        private void borrowers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented");
        }

        private void publishers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented");
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented");
        }

        private void exportHtml_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented");
        }

        private void exportCsv_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sbCsv = new StringBuilder();
            foreach (var item in DataModel.Books)
            {
                sbCsv.AppendFormat("{0};{1};{2}", item.Title, item.Author.FullName, item.CategoriesCsv);
                sbCsv.AppendLine();
            }
            string fileName = System.IO.Path.Combine(Environment.SpecialFolder.Desktop.ToString(), "export.txt");
            File.WriteAllText(fileName, sbCsv.ToString());
            MessageBox.Show(string.Format("Books exported to: {0}", fileName), "Info", MessageBoxButton.OK, MessageBoxImage.Information,
                MessageBoxResult.OK, MessageBoxOptions.None);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Images (.jpg)|*.jpg";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;

                using (Bitmap bitmap = (Bitmap)Bitmap.FromFile(filename))
                {
                    using (Bitmap bitmap2 = bitmap.Scale(new System.Drawing.Size(200, 200)))
                    {
                        // Create source
                        BitmapImage myBitmapImage = new BitmapImage();

                        // BitmapImage.UriSource must be in a BeginInit/EndInit block
                        myBitmapImage.BeginInit();
                        myBitmapImage.UriSource = new Uri(filename);

                        // To save significant application memory, set the DecodePixelWidth or  
                        // DecodePixelHeight of the BitmapImage value of the image source to the desired 
                        // height or width of the rendered image. If you don't do this, the application will 
                        // cache the image as though it were rendered as its normal size rather then just 
                        // the size that is displayed.
                        // Note: In order to preserve aspect ratio, set DecodePixelWidth
                        // or DecodePixelHeight but not both.
                        myBitmapImage.DecodePixelWidth = 200;
                        myBitmapImage.EndInit();

                        using (MemoryStream stream = new MemoryStream())
                        {
                            bitmap2.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            Byte[] bytes = stream.ToArray();
                            ((Book)DataContext).Image = bytes;
                            //(Book)lvItems.SelectedItem
                        }
                    }
                }
            }
        }
    }
}
