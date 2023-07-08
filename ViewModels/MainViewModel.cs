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
        public ImageAreaSelectingViewModel imageAreaSelectingViewModel { get; set; }
        public PlanimetricCircleDrawingViewModel PlanimetricCircleDrawingViewModel { get; set; }

        public ICommand CreateWorkspace { get; private set; }
        public ICommand OpenWorkspaceCommand { get; private set; }
        public ICommand SaveWorkspace { get; private set; }

        public ICommand OpenImageFileCommand { get; private set; }
        public ICommand OpenImageWindowCommand { get; private set; }
        public ICommand OpenImage { get; private set; }

        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }

        private IWindowService imageWindowService;

        public MainViewModel(
            ImageAreaSelectingViewModel imageAreaSelectingViewModel,
            PlanimetricCircleDrawingViewModel planimetricCircleDrawingViewModel,
            IWindowService imageWindowService,
            AppData appData,
            ImageData imageData,
            ImageDisplay imageDisplay)
        {
            this.imageAreaSelectingViewModel = imageAreaSelectingViewModel;
            this.PlanimetricCircleDrawingViewModel = planimetricCircleDrawingViewModel;
            this.imageWindowService = imageWindowService;
            this.appData = appData;
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;

            CreateWorkspace = CreateCommand(
                _ =>
                {
                    appData.CurrentState = AppState.WorkspacePrepared;
                    Workspace.Instance.Initialize();
                },
                _ => appData.CandCreateWorkspace()
            );
            OpenWorkspaceCommand = CreateCommand(
                _ =>
                {
                    appData.CurrentState = AppState.WorkspacePrepared;
                    string filePath = @"D:\Projects\GrainDetector\sample1.dat";
                    Workspace.Instance.Load(filePath);
                },
                _ => this.appData.CanOpenWorkspace());
            SaveWorkspace = CreateCommand(
                _ =>
                {
                    string filePath = @"D:\Projects\GrainDetector\sample1.dat";
                    Workspace.Instance.Save(filePath);
                },
                _ => this.appData.CanSaveWorkspace());
            this.OpenImageFileCommand = CreateCommand(_ => { this.imageData.OpenImageFile(null); });
            this.OpenImageWindowCommand = new OpenImageWindow(imageWindowService);
            this.OpenImage = CreateCommand(_ => {
                OpenImageFileCommand.Execute(null);
                this.imageDisplay.UpdateImage();
                this.imageDisplay.ZoomScale = 1.0;
                OpenImageWindowCommand.Execute(null);
            }, _ => this.appData.CanOpenImage());
            this.ZoomInCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 2.0; },
                _ => this.appData.CanZoomInOut() && this.imageDisplay.ZoomScale < 8.0);
            this.ZoomOutCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 0.5; },
                _ => this.appData.CanZoomInOut() && this.imageDisplay.ZoomScale > 0.125);
        }
    }
}
