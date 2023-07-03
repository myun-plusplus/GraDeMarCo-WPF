using GraDeMarCoWPF.Commands;
using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Views;
using System.Net;
using System.Windows.Input;

namespace GraDeMarCoWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private AppData appData;
        private ImageData imageData;
        private ImageDisplay imageDisplay;

        public ICommand OpenWorkspaceCommand { get; private set; }
        public ICommand SaveWorkspace { get; private set; }

        public ICommand OpenImageFileCommand { get; private set; }
        public ICommand OpenImageWindowCommand { get; private set; }
        public ICommand OpenImage { get; private set; }

        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }

        private IWindowService _openWindowService;

        public MainViewModel(ImageWindow imageWindow)
        {
            this.appData = Workspace.Instance.AppData;
            this.imageData = Workspace.Instance.ImageData;
            this.imageDisplay = Workspace.Instance.ImageDisplay;

            OpenWorkspaceCommand = CreateCommand(_ =>
            {
                string filePath = @"D:\Projects\GrainDetector\sample1.dat";
                Workspace.Instance.Load(filePath);
            }, _ => appData.CanOpenWorkspace());
            SaveWorkspace = CreateCommand(_ =>
            {
                string filePath = @"D:\Projects\GrainDetector\sample1.dat";
                Workspace.Instance.Save(filePath);
            }, _ => appData.CanSaveWorkspace());
            this.OpenImageFileCommand = CreateCommand(_ => { imageData.OpenImageFile(null); });
            this.OpenImageWindowCommand = new OpenImageWindow(new SubWindowService(imageWindow));
            this.OpenImage = CreateCommand(_ => {
                OpenImageFileCommand.Execute(null);
                imageDisplay.UpdateImage();
                imageDisplay.ZoomScale = 1.0;
                OpenImageWindowCommand.Execute(null);
            }, _ => appData.CanOpenImage());
            this.ZoomInCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 2.0; },
                _ => appData.CanZoomInOut() && this.imageDisplay.ZoomScale < 8.0);
            this.ZoomOutCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 0.5; },
                _ => appData.CanZoomInOut() && this.imageDisplay.ZoomScale > 0.125);
        }
    }
}
