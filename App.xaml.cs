using GraDeMarCo;
using GraDeMarCoWPF.ViewModels;
using GraDeMarCoWPF.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            var imageData = new ImageData();

            var imageViewModel = new ImageViewModel();

            ImageWindow imageWindow = new ImageWindow(imageViewModel);
            MainWindow mainWindow = new MainWindow (imageWindow);
            mainWindow.Show ();
        }
    }
}
