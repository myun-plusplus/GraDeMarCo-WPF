using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using GraDeMarCoWPF.ViewModels;
using GraDeMarCoWPF.Views;
using System.Windows;

namespace GraDeMarCoWPF
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Models
            var appData = Workspace.Instance.AppData;

            var imageData = Workspace.Instance.ImageData;
            var imageDisplay = Workspace.Instance.ImageDisplay;
            var imageIO = Workspace.Instance.ImageIO;

            var imageArea = Workspace.Instance.ImageArea;
            var imageAreaDrawingTool = Workspace.Instance.ImageAreaDrawingTool;
            var imageAreaSelecting = Workspace.Instance.ImageAreaSelecting;

            var planimetricCircle = Workspace.Instance.PlanimetricCircle;
            var planimetricCircleDrawingTool = Workspace.Instance.PlanimetricCircleDrawingTool;
            var planimetricCircleDrawing = Workspace.Instance.PlanimetricCircleDrawing;

            var imageFilterOptions = Workspace.Instance.ImageFilterOptions;
            var imageFiltering = Workspace.Instance.ImageFiltering;

            var imageBinarizeOptions = Workspace.Instance.ImageBinarizeOptions;
            var imageBinarizing = Workspace.Instance.ImageBinarizing;

            var grainDetectingOptions = Workspace.Instance.GrainDetectingOptions;
            var grainInCircleDotDrawingTool = Workspace.Instance.GrainInCircleDotDrawingTool;
            var grainOnCircleDotDrawingTool = Workspace.Instance.GrainOnCircleDotDrawingTool;
            var detectedDotData = Workspace.Instance.DetectedDotData;
            var grainDetecting = Workspace.Instance.GrainDetecting;

            var drawnDotData = Workspace.Instance.DrawnDotData;
            var drawnDotDrawingTool = Workspace.Instance.DrawnDotDrawingTool;
            var dotDrawing = Workspace.Instance.DotDrawing;

            var appStateHandler = Workspace.Instance.AppStateHandler;
            var imageModification = Workspace.Instance.ImageModification;

            var openWorkspaceDialogService = new OpenFileDialogService(
                "開くワークスペースを選択してください",
                "DATファイル(*.dat)|*.dat|すべてのファイル(*.*)|*.*");
            var saveWorkspaceDialogService = new SaveFileDialogService(
                "ワークスペースの保存先を選択してください",
                "DATファイル(*.dat)|*.dat|すべてのファイル(*.*)|*.*");
            var openImageFileDialogService = new OpenFileDialogService(
                "開く画像ファイルを選択してください",
                "画像ファイル(*.bmp,*.exif,*.gif,*.jpg,*.png,*.tiff)|*.bmp;*.exif;*.gif;*.jpg;*.png;*.tiff|" +
                "BMPファイル(*.bmp)|*.bmp|" +
                "EXIFファイル(*.exif)|*.exif|" +
                "GIFファイル(*.gif)|*.gif|" +
                "JPEGファイル(*.jpg)|*.jpg|" +
                "PNGファイル(*.png)|*.png|" +
                "TIFFファイル(*.tiff)|*.tiff|" +
                "すべてのファイル(*.*)|*.*");
            var saveImageDialogService = new SaveFileDialogService(
                "画像の保存先を選択してください",
                "BMPファイル(*.bmp)|*.bmp|" +
                "EXIFファイル(*.exif)|*.exif|" +
                "GIFファイル(*.gif)|*.gif|" +
                "JPEGファイル(*.jpg)|*.jpg|" +
                "PNGファイル(*.png)|*.png|" +
                "TIFFファイル(*.tiff)|*.tiff|" +
                "すべてのファイル(*.*)|*.*");
            var colorDialogService = new ColorDialogService();

            // ViewModels
            var imageViewModel = new ImageViewModel(
                appData,
                imageData,
                imageDisplay,
                imageAreaSelecting,
                appStateHandler,
                imageModification);
            var imageAreaSelectingViewModel = new ImageAreaSelectingViewModel(
                appData,
                imageData,
                imageArea,
                imageAreaSelecting);
            var planimetricCircleDrawingViewModel = new PlanimetricCircleDrawingViewModel(
                colorDialogService,
                appData,
                imageArea,
                planimetricCircle,
                planimetricCircleDrawingTool,
                planimetricCircleDrawing);
            var imageFIlterViewModel = new ImageFilteringViewModel(
                appData,
                imageDisplay,
                imageFilterOptions,
                imageFiltering,
                imageBinarizing,
                grainDetecting);
            var imageBinarizingViewModel = new ImageBinarizingViewModel(
                appData,
                imageDisplay,
                imageBinarizeOptions,
                imageBinarizing,
                grainDetecting);
            var grainDetectingViewModel = new GrainDetectingViewModel(
                colorDialogService,
                appData,
                imageDisplay,
                grainDetectingOptions,
                grainInCircleDotDrawingTool,
                grainOnCircleDotDrawingTool,
                grainDetecting);
            var dotDrawingViewModel = new DotDrawingViewModel(
                colorDialogService,
                appData,
                imageDisplay,
                drawnDotDrawingTool,
                detectedDotData,
                drawnDotData,
                dotDrawing);

            var imageWindow = new ImageWindow()
            {
                DataContext = imageViewModel
            };

            var imageWindowService = new WindowService(imageWindow);

            var mainViewModel = new MainViewModel(
                imageAreaSelectingViewModel,
                planimetricCircleDrawingViewModel,
                imageFIlterViewModel,
                imageBinarizingViewModel,
                grainDetectingViewModel,
                dotDrawingViewModel,
                appData,
                imageData,
                imageDisplay,
                imageIO,
                imageModification,
                imageWindowService,
                openWorkspaceDialogService,
                saveWorkspaceDialogService,
                openImageFileDialogService,
                saveImageDialogService);

            var mainWindow = new MainWindow()
            {
                DataContext = mainViewModel
            };
            mainWindow.SourceInitialized += (s, ea) => { imageWindow.Owner = mainWindow; };

            mainWindow.Show ();
        }
    }
}
