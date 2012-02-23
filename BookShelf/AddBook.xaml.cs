using System;
using System.IO;
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
    public partial class AddBook : Window
    {
        private BookAddViewModel DataModel { get; set; }

        public AddBook()
        {
            this.DataModel = new BookAddViewModel();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Book();
        }

        public IEnumerable<Author> Authors
        {
            get { return this.DataModel.Authors; }
        }

        public IEnumerable<Category> Categories
        {
            get { return this.DataModel.Categories; }
        }

        int noOfErrorsOnScreen = 0;

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                noOfErrorsOnScreen++;
            else
                noOfErrorsOnScreen--;
        }

        private void AddBook_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = noOfErrorsOnScreen == 0;
            e.Handled = true;
        }

        private void AddBook_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var item = (Book)(this.DataContext);

            // TODO: How to do it using binding?
            item.Categories = new List<Category>();
            foreach (Category c in lbCategories.SelectedItems)
                item.Categories.Add(c);

            StoreRepository.Book.Save(item);
            e.Handled = true;
            this.Close();
        }

        private void chooseImage_Click(object sender, RoutedEventArgs e)
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
                        image1.Source = myBitmapImage;

                        using (MemoryStream stream = new MemoryStream())
                        {
                            bitmap2.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            Byte[] bytes = stream.ToArray();
                            ((Book)DataContext).Image = bytes;
                        }
                    }
                }
            }
        }
    }
}
