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
        private AppData appData;
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private ImageAreaSelecting imageAreaSelecting;
        private AppStateHandler appStateHandler;

        public WriteableBitmap DisplayedImage
        {
            get
            {
                switch (appData.CurrentState)
                {
                    case AppState.ImageFiltering:
                        return imageData.FilteredImage;
                    case AppState.ImageBinarizing:
                        return imageData.BinarizedImage;
                    default:
                        return imageData.OriginalImage;
                }
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

        public ImageViewModel(
            AppData appData,
            ImageData imageData,
            ImageDisplay imageDisplay,
            ImageAreaSelecting imageAreaSelecting,
            AppStateHandler appStateHandler)
        {
            this.appData = Workspace.Instance.AppData;
            this.imageData = imageData;
            this.imageDisplay = Workspace.Instance.ImageDisplay;
            this.imageAreaSelecting = Workspace.Instance.ImageAreaSelecting;
            this.appStateHandler = Workspace.Instance.AppStateHandler;

            appData.PropertyChanged += appData_PropertyChanged;
            this.imageDisplay.PropertyChanged += imageDisplay_PropertyChanged;
            appStateHandler.PropertyChanged += appStateHandler_PropertyChanged;

            this.ZoomInCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 2.0; },
                _ => this.appData.CanZoomInOut() && this.imageDisplay.ZoomScale < 8.0);
            this.ZoomOutCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 0.5; },
                _ => this.appData.CanZoomInOut() && this.imageDisplay.ZoomScale > 0.125);
            DrawOnRenderCommand = CreateCommand(
                drawingContext => appStateHandler.DrawOnRender(drawingContext as DrawingContext));
            LeftClickCommand = CreateCommand(
                location => appStateHandler.LeftClick((Point)location),
                _ => this.appData.IsClickEnabled());
            MouseMoveCommand = CreateCommand(
                location => appStateHandler.MouseMove((Point)location),
                _ => this.appData.IsMouseMoveEnabled());
        }

        private void appData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("");
        }

        private void imageDisplay_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
            NotifyPropertyChanged(GetName.Of(() => ImageWidth));
            NotifyPropertyChanged(GetName.Of(() => ImageHeight));
        }

        private void appStateHandler_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }
    }
}
