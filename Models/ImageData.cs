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
                if (value != null)
                {
                    _originalImage = new WriteableBitmap(new FormatConvertedBitmap(value, PixelFormats.Bgr32, null, 0));
                    _filteredImage = new WriteableBitmap(_originalImage);
                }
            }
        }

        public WriteableBitmap FilteredImage
        {
            get { return _filteredImage; }
            set
            {
                _filteredImage = new WriteableBitmap(new FormatConvertedBitmap(value, PixelFormats.Bgr32, null, 0));
            }
        }

        private WriteableBitmap _originalImage;
        private WriteableBitmap _filteredImage;
    }
}
