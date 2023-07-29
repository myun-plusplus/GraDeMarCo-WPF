using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraDeMarCoWPF.Models
{
    public class ImageData
    {
        public readonly static PixelFormat PixelFormat = PixelFormats.Bgr32;

        public WriteableBitmap OriginalImage
        {
            get { return _originalImage; }
            set
            {
                if (value != null)
                {
                    _originalImage = new WriteableBitmap(new FormatConvertedBitmap(value, PixelFormat, null, 0));
                    _filteredImage = new WriteableBitmap(_originalImage);
                }
            }
        }

        public WriteableBitmap CircledImage
        {
            get { return _circledImage; }
            set
            {
                _circledImage = new WriteableBitmap(new FormatConvertedBitmap(value, PixelFormat, null, 0));
            }
        }

        public WriteableBitmap FilteredImage
        {
            get { return _filteredImage; }
            set
            {
                _filteredImage = new WriteableBitmap(new FormatConvertedBitmap(value, PixelFormat, null, 0));
            }
        }

        public WriteableBitmap BinarizedImage
        {
            get { return _binarizedImage; }
            set
            {
                _binarizedImage = new WriteableBitmap(new FormatConvertedBitmap(value, PixelFormats.Bgr32, null, 0));
            }
        }

        public void Initialize()
        {
            _originalImage = new WriteableBitmap(64, 64, 8, 8, PixelFormat, null);
            _circledImage = _originalImage;
            _filteredImage = _originalImage;
            _binarizedImage = _originalImage;
        }

        private WriteableBitmap _originalImage;
        private WriteableBitmap _circledImage;
        private WriteableBitmap _filteredImage;
        private WriteableBitmap _binarizedImage;
    }
}
