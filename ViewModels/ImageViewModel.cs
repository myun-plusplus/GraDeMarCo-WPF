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
                return imageDisplay.DisplayedImage;
            }
            set
            {
                imageDisplay.DisplayedImage = value;
            }
        }

        public double ZoomScale
        {
            get
            {
                return imageDisplay.ZoomScale;
            }
            set
            {
                imageDisplay.ZoomScale = value;
            }
        }

        public double ImageWidth
        {
            get
            {
                if (DisplayedImage != null)
                {
                    return DisplayedImage.Width * ZoomScale;
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public double ImageHeight
        {
            get
            {
                if (DisplayedImage != null)
                {
                    return DisplayedImage.Height * ZoomScale;
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }
        public ICommand DrawOnRenderCommand { get; private set; }
        public ICommand LeftClickCommand { get; private set; }
        public ICommand RightClickCommand { get; private set; }
        public ICommand MouseMoveCommand { get; private set; }


        public ICommand TestClickCommand { get; private set; }

        private AppData appData;
        private ImageDisplay imageDisplay;
        private ImageAreaSelecting imageAreaSelecting;

        public ImageViewModel()
        {
            this.appData = Workspace.Instance.AppData;
            this.imageDisplay = Workspace.Instance.ImageDisplay;
            this.imageAreaSelecting = Workspace.Instance.ImageAreaSelecting;

            imageDisplay.PropertyChanged += imageDisplay_PropertyChanged;

            this.ZoomInCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 2.0; },
                _ => appData.CanZoomInOut() && this.imageDisplay.ZoomScale < 8.0);
            this.ZoomOutCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 0.5; },
                _ => appData.CanZoomInOut() && this.imageDisplay.ZoomScale > 0.125);
            DrawOnRenderCommand = CreateCommand(drawingContext =>
            {
                appData.DrawOnRender(drawingContext as DrawingContext);
            });
            LeftClickCommand = CreateCommand(
                location => this.imageAreaSelecting.Click((Point)location),
                _ => appData.IsClickEnabled());
            MouseMoveCommand = CreateCommand(
                location => this.imageAreaSelecting.MouseMove((Point)location),
                _ => appData.IsMouseMoveEnabled());

            TestClickCommand = new TestClick(this);
        }

        private void imageDisplay_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
            NotifyPropertyChanged(GetName.Of(() => ImageWidth));
            NotifyPropertyChanged(GetName.Of(() => ImageHeight));
        }
    }
}
