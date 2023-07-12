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
            this.SourceInitialized += (s, e) =>
            {
                viewModel.PropertyChanged += viewModel_OnPropertyChanged;
            };
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
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                viewModel.LeftClickCommand.Execute(e.GetPosition(myPictureBox));

                this.myRenderBox.InvalidateVisual();
            }
        }

        private void MyPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            viewModel.MouseMoveCommand.Execute(e.GetPosition(myPictureBox));

            this.myRenderBox.InvalidateVisual();
        }

        private void viewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.myRenderBox.InvalidateVisual();
        }
    }
}
