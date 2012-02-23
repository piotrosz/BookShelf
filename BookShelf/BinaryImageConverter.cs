using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.IO;
using System.Windows.Media.Imaging;
using System.Globalization;

namespace BookShelf
{
    public class BinaryImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage image = null;

            if (value != null && value is byte[])
            {
                byte[] bytes = value as byte[];
                var stream = new MemoryStream(bytes);
                image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                //image.UriSource = new Uri(@"D:\temp\Desert.jpg");
                image.EndInit();
            }
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
