using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraDeMarCoWPF.Models
{
    public class ImageData
    {
        public WriteableBitmap OriginalImage
        {
            get { return _originalImage; }
            set
            {
                _originalImage = new WriteableBitmap(new FormatConvertedBitmap(value, PixelFormats.Bgr32, null, 0));
            }
        }

        private WriteableBitmap _originalImage;

        public void OpenImageFile(string filePath)
        {
            var bitmapSource = new BitmapImage(new Uri(@"D:\Projects\GrainDetector\sample1.jpg"));
            OriginalImage = new WriteableBitmap(bitmapSource);
        }
    }
}
