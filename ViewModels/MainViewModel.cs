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
        ImageModification imageModification;

        public ImageAreaSelectingViewModel imageAreaSelectingViewModel { get; set; }
        public PlanimetricCircleDrawingViewModel PlanimetricCircleDrawingViewModel { get; set; }
        public ImageFilteringViewModel ImageFilteringViewModel { get; private set; }
        public ImageBinarizingViewModel ImageBinarizingViewModel { get; private set; }
        public GrainDetectingViewModel GrainDetectingViewModel { get; private set; }
        public DotDrawingViewModel DotDrawingViewModel { get; private set; }
        public DotCountingViewModel DotCountingViewModel { get; private set; }

        public bool ImageAreaIsDisplayedOnImage
        {
            get
            {
                return imageModification.ImageModificationlags.HasFlag(ImageModificationFlags.ImageArea);
            }
            set
            {
                if (value)
                {
                    imageModification.ImageModificationlags |= ImageModificationFlags.ImageArea;
                }
                else
                {
                    imageModification.ImageModificationlags &= ~ImageModificationFlags.ImageArea;
                }
                NotifyPropertyChanged(GetName.Of(() => ImageAreaIsDisplayedOnImage));
            }
        }

        public bool PlanimetricCircleIsDisplayedOnImage
        {
            get
            {
                return imageModification.ImageModificationlags.HasFlag(ImageModificationFlags.PlanimetricCircle);
            }
            set
            {
                if (value)
                {
                    imageModification.ImageModificationlags |= ImageModificationFlags.PlanimetricCircle;
                }
                else
                {
                    imageModification.ImageModificationlags &= ~ImageModificationFlags.PlanimetricCircle;
                }
                NotifyPropertyChanged(GetName.Of(() => PlanimetricCircleIsDisplayedOnImage));
            }
        }

        public bool ImageModifyingIsDisplayed
        {
            get
            {
                return imageModification.ImageModificationlags.HasFlag(ImageModificationFlags.ImageModifying);
            }
            set
            {
                if (value)
                {
                    imageModification.ImageModificationlags |= ImageModificationFlags.ImageModifying;
                }
                else
                {
                    imageModification.ImageModificationlags &= ~ImageModificationFlags.ImageModifying;
                }
                NotifyPropertyChanged(GetName.Of(() => ImageModifyingIsDisplayed));
            }
        }

        public bool DrawnDotsAreDisplayed
        {
            get
            {
                return imageModification.ImageModificationlags.HasFlag(ImageModificationFlags.DrawnDots);
            }
            set
            {
                if (value)
                {
                    imageModification.ImageModificationlags |= ImageModificationFlags.DrawnDots;
                }
                else
                {
                    imageModification.ImageModificationlags &= ~ImageModificationFlags.DrawnDots;
                }
                NotifyPropertyChanged(GetName.Of(() => DrawnDotsAreDisplayed));
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
        public ICommand ApplyImageModification { get; private set; }

        private IWindowService imageWindowService;

        public MainViewModel(
            ImageAreaSelectingViewModel imageAreaSelectingViewModel,
            PlanimetricCircleDrawingViewModel planimetricCircleDrawingViewModel,
            ImageFilteringViewModel imageFilteringViewModel,
            ImageBinarizingViewModel imageBinarizingViewModel,
            GrainDetectingViewModel grainDetectingViewModel,
            DotDrawingViewModel dotDrawingViewModel,
            DotCountingViewModel dotCountingViewModel,
            AppData appData,
            ImageData imageData,
            ImageDisplay imageDisplay,
            ImageIO imageIO,
            ImageModification appStateHandler,
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
            DotCountingViewModel = dotCountingViewModel;
            this.imageWindowService = imageWindowService;
            this.appData = appData;
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageModification = appStateHandler;

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
            ApplyImageModification = new ApplyImageProcessing(appData, imageDisplay, imageModification);
        }
    }
}
