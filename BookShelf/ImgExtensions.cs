using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BookShelf
{
    public static class ImgExtensions
    {
        public static Bitmap Scale(this Image image, Size size)
        {
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;

            float nPercentW = (size.Width / (float)sourceWidth);
            float nPercentH = (size.Height / (float)sourceHeight);

            float nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bitmapDest = new Bitmap(destWidth, destHeight, image.PixelFormat);

            bitmapDest.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(bitmapDest))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphics.DrawImage(image, 0, 0, bitmapDest.Width, bitmapDest.Height);
            }

            return bitmapDest;
        }
    }
}
