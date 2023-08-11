using System.Windows;
using System.Windows.Media.Imaging;

namespace GraDeMarCoWPF.Models
{
    public class ImageDisplay : BindableBase
    {
        private ImageData imageData;

        public WriteableBitmap DisplayedImage
        {
            get { return _displayedImage; }
            set
            {
                _displayedImage = value;
                NotifyPropertyChanged(GetName.Of(() => DisplayedImage));
            }
        }

        public double ZoomScale
        {
            get
            {
                return _zoomScale;
            }
            set
            {
                _zoomScale = value;
                NotifyPropertyChanged(GetName.Of(() => ZoomScale));
            }
        }

        private WriteableBitmap _displayedImage;
        private double _zoomScale;

        public ImageDisplay(ImageData imageData)
        {
            this.imageData = imageData;
        }

        public void UpdateImage()
        {
            DisplayedImage = imageData.OriginalImage.Clone();
            ZoomScale = 1.0;
        }

        public void RefreshRendering()
        {
            NotifyAllPropertyChanged();
        }

        public Point GetAbsoluteLocation(Point location)
        {
            return new Point(
                location.X * DisplayedImage.PixelWidth / DisplayedImage.Width,
                location.Y * DisplayedImage.PixelHeight / DisplayedImage.Height);
        }

        public Point GetRelativeLocation(Point location)
        {
            return new Point(
                location.X * DisplayedImage.Width / DisplayedImage.PixelWidth,
                location.Y * DisplayedImage.Height / DisplayedImage.PixelHeight);
        }

        public Point GetUnzoomedLocation(Point location)
        {
            return new Point(location.X / ZoomScale, location.Y / ZoomScale);
        }
    }
}
