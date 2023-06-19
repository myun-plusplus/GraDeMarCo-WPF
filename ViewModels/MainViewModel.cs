using GraDeMarCoWPF.Commands;
using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Views;
using System.Windows.Input;

namespace GraDeMarCoWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ImageData imageData;
        private ImageDisplay imageDisplay;

        public ICommand OpenImageFileCommand { get; private set; }
        public ICommand OpenImageWindowCommand { get; private set; }
        public ICommand OpenImage { get; private set; }

        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }

        private IOpenWindowService _openWindowService;

        public MainViewModel(ImageWindow imageWindow)
        {
            this.imageData = Workspace.Instance.ImageData;
            this.imageDisplay = Workspace.Instance.ImageDisplay;

            this.OpenImageFileCommand = CreateCommand(_ => { imageData.OpenImageFile(null); });
            this.OpenImageWindowCommand = new OpenImageWindow(new OpenSubWindowService(imageWindow));
            this.OpenImage = CreateCommand(_ => {
                OpenImageFileCommand.Execute(null);
                imageDisplay.UpdateImage();
                imageDisplay.ZoomScale = 1.0;
                OpenImageWindowCommand.Execute(null);
            });
            this.ZoomInCommand = CreateCommand(_ => { this.imageDisplay.ZoomScale *= 2.0; }, _ => this.imageDisplay.ZoomScale < 8.0);
            this.ZoomOutCommand = CreateCommand(_ => { this.imageDisplay.ZoomScale *= 0.5; }, _ => this.imageDisplay.ZoomScale > 0.125);
        }
    }
}
