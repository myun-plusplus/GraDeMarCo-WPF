using System.Windows;
using GraDeMarCoWPF.ViewModels;

namespace GraDeMarCoWPF.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ImageWindow imageWindow)
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(imageWindow);

            this.SourceInitialized += (s, e) => { imageWindow.Owner = this; };
        }
    }
}
