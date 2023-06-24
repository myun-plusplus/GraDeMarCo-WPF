using GraDeMarCoWPF.Commands;
using GraDeMarCoWPF.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraDeMarCoWPF.ViewModels
{
    public class ImageViewModel : ViewModelBase
    {
        public WriteableBitmap DisplayedImage
        {
            get
            {
                return _displayedImage;
            }
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

                if (DisplayedImage != null)
                {
                    ImageWidth = DisplayedImage.Width * value;
                    ImageHeight = DisplayedImage.Height * value;
                }
            }
        }

        public double ImageWidth
        {
            get { return _imageWidth; }
            set
            {
                _imageWidth = value;
                NotifyPropertyChanged(GetName.Of(() => ImageWidth));
            }
        }

        public double ImageHeight
        {
            get { return _imageHeight; }
            set
            {
                _imageHeight = value;
                NotifyPropertyChanged(GetName.Of(() => ImageHeight));
            }
        }

        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }
        public ICommand DisplayImageAreaCommand { get; private set; }
        public ICommand LeftClickCommand { get; private set; }
        public ICommand RightClickCommand { get; private set; }
        public ICommand MouseMoveCommand { get; private set; }


        public ICommand TestClickCommand { get; private set; }

        private ImageDisplay imageDisplay;
        private ImageAreaSelecting imageAreaSelecting;

        private WriteableBitmap _displayedImage;
        private WriteableBitmap _dummyImage;
        private double _zoomScale;
        private double _imageWidth;
        private double _imageHeight;

        public ImageViewModel()
        {
            this.imageDisplay = Workspace.Instance.ImageDisplay;
            this.imageDisplay.PropertyChanged += imageDisplay_PropertyChanged;
            this.imageAreaSelecting = Workspace.Instance.ImageAreaSelecting;

            this.ZoomInCommand = CreateCommand(_ => { this.imageDisplay.ZoomScale *= 2.0; }, _ => this.imageDisplay.ZoomScale < 8.0);
            this.ZoomOutCommand = CreateCommand(_ => { this.imageDisplay.ZoomScale *= 0.5; }, _ => this.imageDisplay.ZoomScale > 0.125);
            DisplayImageAreaCommand = CreateCommand(drawingContext =>
            {
                this.imageAreaSelecting.DrawOnRender(drawingContext as DrawingContext);
            });
            this.LeftClickCommand = CreateCommand(location => this.imageAreaSelecting.Click((Point)location));
            this.MouseMoveCommand = CreateCommand(location => this.imageAreaSelecting.MouseMove((Point)location));

            this.TestClickCommand = new TestClick(this);
        }

        private void imageDisplay_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GetName.Of(() => imageDisplay.DisplayedImage))
            {
                this.DisplayedImage = imageDisplay.DisplayedImage;
            }
            else if (e.PropertyName == GetName.Of(() => imageDisplay.ZoomScale))
            {
                this.ZoomScale = imageDisplay.ZoomScale;
            }
        }
    }
}
