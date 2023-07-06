using GraDeMarCoWPF.Models;
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

            var imageArea = Workspace.Instance.ImageArea;
            var imageAreaDrawingTool = Workspace.Instance.ImageAreaDrawingTool;
            var imageAreaSelecting = Workspace.Instance.ImageAreaSelecting;

            var planimetricCircle = Workspace.Instance.PlanimetricCircle;
            var planimetricCircleDrawingTool = Workspace.Instance.PlanimetricCircleDrawingTool;
            var planimetricCircleDrawing = Workspace.Instance.PlanimetricCircleDrawing;

            var appStateHandler = Workspace.Instance.AppStateHandler;

            var colorDialogService = new ColorDialogService();

            // ViewModels
            var imageViewModel = new ImageViewModel(
                appData,
                imageDisplay,
                imageAreaSelecting,
                appStateHandler);
            var imageAreaSelectingViewModel = new ImageAreaSelectingViewModel(
                appData,
                imageArea,
                imageAreaSelecting);
            var planimetricCircleDrawingViewModel = new PlanimetricCircleDrawingViewModel(
                colorDialogService,
                appData,
                imageArea,
                planimetricCircle,
                planimetricCircleDrawingTool,
                planimetricCircleDrawing);

            var imageWindow = new ImageWindow()
            {
                DataContext = imageViewModel
            };

            var imageWindowService = new SubWindowService(imageWindow);

            var mainViewModel = new MainViewModel(
                imageAreaSelectingViewModel,
                planimetricCircleDrawingViewModel,
                imageWindowService,
                appData,
                imageData,
                imageDisplay);

            var mainWindow = new MainWindow()
            {
                DataContext = mainViewModel
            };
            mainWindow.SourceInitialized += (s, ea) => { imageWindow.Owner = mainWindow; };

            mainWindow.Show ();
        }
    }
}
