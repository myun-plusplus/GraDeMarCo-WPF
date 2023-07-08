using GraDeMarCoWPF.Commands;
using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
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
        public ICommand OpenWorkspace { get; private set; }
        public ICommand OverwriteWorkspace { get; private set; }
        public ICommand SaveAsWorkspace { get; private set; }

        public ICommand OpenImage { get; private set; }

        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }

        private IWindowService imageWindowService;

        public MainViewModel(
            ImageAreaSelectingViewModel imageAreaSelectingViewModel,
            PlanimetricCircleDrawingViewModel planimetricCircleDrawingViewModel,
            AppData appData,
            ImageData imageData,
            ImageDisplay imageDisplay,
            IWindowService imageWindowService,
            IOpenFileDialogService openWorkspaceDialogService,
            ISaveFileDialogService saveFileDialogService,
            IOpenFileDialogService openImageFileDialogService)
        {
            this.imageAreaSelectingViewModel = imageAreaSelectingViewModel;
            this.PlanimetricCircleDrawingViewModel = planimetricCircleDrawingViewModel;
            this.imageWindowService = imageWindowService;
            this.appData = appData;
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;

            CreateWorkspace = new CreateWorkspace(appData);
            OpenWorkspace = new OpenWorkspace(appData, openWorkspaceDialogService);
            OverwriteWorkspace = new OverwriteWorkspace(appData);
            SaveAsWorkspace = new SaveAsWorkspace(appData, saveFileDialogService);
            OpenImage = new OpenImage(appData, imageData, imageDisplay, imageWindowService, openImageFileDialogService);

            this.ZoomInCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 2.0; },
                _ => this.appData.CanZoomInOut() && this.imageDisplay.ZoomScale < 8.0);
            this.ZoomOutCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 0.5; },
                _ => this.appData.CanZoomInOut() && this.imageDisplay.ZoomScale > 0.125);
        }
    }
}
