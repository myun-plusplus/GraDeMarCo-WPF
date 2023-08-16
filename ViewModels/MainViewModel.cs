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
        AppStateHandler appStateHandler;

        public ImageAreaSelectingViewModel imageAreaSelectingViewModel { get; set; }
        public PlanimetricCircleDrawingViewModel PlanimetricCircleDrawingViewModel { get; set; }
        public ImageFilteringViewModel ImageFilteringViewModel { get; private set; }
        public ImageBinarizingViewModel ImageBinarizingViewModel { get; private set; }
        public GrainDetectingViewModel GrainDetectingViewModel { get; private set; }
        public DotDrawingViewModel DotDrawingViewModel { get; private set; }

        public bool ImageAreaIsDisplayedOnImage
        {
            get
            {
                return appStateHandler.ImageProcessingFlags.HasFlag(ImageProcessingFlags.ImageArea);
            }
            set
            {
                if (value)
                {
                    appStateHandler.ImageProcessingFlags |= ImageProcessingFlags.ImageArea;
                }
                else
                {
                    appStateHandler.ImageProcessingFlags &= ~ImageProcessingFlags.ImageArea;
                }
                NotifyPropertyChanged(GetName.Of(() => ImageAreaIsDisplayedOnImage));
            }
        }

        public bool PlanimetricCircleIsDisplayedOnImage
        {
            get
            {
                return appStateHandler.ImageProcessingFlags.HasFlag(ImageProcessingFlags.PlanimetricCircle);
            }
            set
            {
                if (value)
                {
                    appStateHandler.ImageProcessingFlags |= ImageProcessingFlags.PlanimetricCircle;
                }
                else
                {
                    appStateHandler.ImageProcessingFlags &= ~ImageProcessingFlags.PlanimetricCircle;
                }
                NotifyPropertyChanged(GetName.Of(() => PlanimetricCircleIsDisplayedOnImage));
            }
        }

        public bool ImageModifyingIsDisplayed
        {
            get
            {
                return appStateHandler.ImageProcessingFlags.HasFlag(ImageProcessingFlags.ImageModifying);
            }
            set
            {
                if (value)
                {
                    appStateHandler.ImageProcessingFlags |= ImageProcessingFlags.ImageModifying;
                }
                else
                {
                    appStateHandler.ImageProcessingFlags &= ~ImageProcessingFlags.ImageModifying;
                }
                NotifyPropertyChanged(GetName.Of(() => ImageModifyingIsDisplayed));
            }
        }

        public ICommand CreateWorkspace { get; private set; }
        public ICommand OpenWorkspace { get; private set; }
        public ICommand OverwriteWorkspace { get; private set; }
        public ICommand SaveAsWorkspace { get; private set; }

        public ICommand OpenImage { get; private set; }
        public ICommand SaveImage { get; private set; }

        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }

        private IWindowService imageWindowService;

        public MainViewModel(
            ImageAreaSelectingViewModel imageAreaSelectingViewModel,
            PlanimetricCircleDrawingViewModel planimetricCircleDrawingViewModel,
            ImageFilteringViewModel imageFilteringViewModel,
            ImageBinarizingViewModel imageBinarizingViewModel,
            GrainDetectingViewModel grainDetectingViewModel,
            DotDrawingViewModel dotDrawingViewModel,
            AppData appData,
            ImageData imageData,
            ImageDisplay imageDisplay,
            ImageIO imageIO,
            AppStateHandler appStateHandler,
            IWindowService imageWindowService,
            IOpenFileDialogService openWorkspaceDialogService,
            ISaveFileDialogService saveWorkspaceDialogService,
            IOpenFileDialogService openImageFileDialogService,
            ISaveFileDialogService saveImageDialogService)
        {
            this.imageAreaSelectingViewModel = imageAreaSelectingViewModel;
            this.PlanimetricCircleDrawingViewModel = planimetricCircleDrawingViewModel;
            ImageFilteringViewModel = imageFilteringViewModel;
            ImageBinarizingViewModel = imageBinarizingViewModel;
            GrainDetectingViewModel = grainDetectingViewModel;
            DotDrawingViewModel = dotDrawingViewModel;
            this.imageWindowService = imageWindowService;
            this.appData = appData;
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.appStateHandler = appStateHandler;

            CreateWorkspace = new CreateWorkspace(appData);
            OpenWorkspace = new OpenWorkspace(appData, openWorkspaceDialogService);
            OverwriteWorkspace = new OverwriteWorkspace(appData);
            SaveAsWorkspace = new SaveAsWorkspace(appData, saveWorkspaceDialogService);
            OpenImage = new OpenImage(appData, imageData, imageDisplay, imageIO, imageWindowService, openImageFileDialogService);
            SaveImage = new SaveImage(appData, imageIO, saveImageDialogService);
            this.ZoomInCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 2.0; },
                _ => this.appData.CanZoomInOut() && this.imageDisplay.ZoomScale < 8.0);
            this.ZoomOutCommand = CreateCommand(
                _ => { this.imageDisplay.ZoomScale *= 0.5; },
                _ => this.appData.CanZoomInOut() && this.imageDisplay.ZoomScale > 0.125);
        }
    }
}
