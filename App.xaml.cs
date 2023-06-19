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
            var imageWindow = new ImageWindow();
            var mainWindow = new MainWindow(imageWindow);
            mainWindow.Show ();
        }
    }
}
