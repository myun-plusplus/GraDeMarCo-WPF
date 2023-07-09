﻿using GraDeMarCoWPF.Models;
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

            var appStateHandler = Workspace.Instance.AppStateHandler;

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
                imageDisplay,
                imageAreaSelecting,
                appStateHandler);
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

            var imageWindow = new ImageWindow()
            {
                DataContext = imageViewModel
            };

            var imageWindowService = new SubWindowService(imageWindow);

            var mainViewModel = new MainViewModel(
                imageAreaSelectingViewModel,
                planimetricCircleDrawingViewModel,
                appData,
                imageData,
                imageDisplay,
                imageIO,
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
