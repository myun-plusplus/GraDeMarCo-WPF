using GraDeMarCoWPF.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GraDeMarCoWPF.Views
{
    /// <summary>
    /// ImageWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ImageWindow : Window
    {
        private ImageViewModel viewModel
        {
            get { return this.DataContext as ImageViewModel; }
        }

        public ImageWindow()
        {
            InitializeComponent();
            this.DataContext = new ImageViewModel();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;

                if (e.Delta > 0 && viewModel.ZoomInCommand.CanExecute(null))
                {
                    viewModel.ZoomInCommand.Execute(null);
                }
                else if (e.Delta < 0 && viewModel.ZoomOutCommand.CanExecute(null))
                {
                    viewModel.ZoomOutCommand.Execute(null);
                }
            }
        }

        private void MyPictureBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.TestClickCommand.Execute(e);
        }
    }
}
