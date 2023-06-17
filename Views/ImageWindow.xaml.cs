using GraDeMarCoWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GraDeMarCoWPF.Views
{
    /// <summary>
    /// ImageWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ImageWindow : Window
    {
        private ImageViewModel viewModel;

        public ImageWindow(ImageViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.viewModel = viewModel;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //if (Keyboard.Modifiers == ModifierKeys.Control)
            //{
            //    if (e.Delta > 0 && viewModel.ZoomInCommand.CanExecute(null))
            //    {
            //        viewModel.ZoomInCommand.Execute(null);
            //    }
            //    else if (e.Delta < 0 && viewModel.ZoomOutCommand.CanExecute(null))
            //    {
            //        viewModel.ZoomOutCommand.Execute(null);
            //    }
            //}
        }

        private void MyPictureBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.TestClickCommand.Execute(e);
        }
    }
}
