using GraDeMarCoWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GraDeMarCoWPF.Commands
{
    public class TestClick : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ImageViewModel imageViewModel;

        private bool flag = false;

        public TestClick(ImageViewModel imageViewModel)
        {
            this.imageViewModel = imageViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var e = parameter as MouseButtonEventArgs;
            if (flag)
            {
                int width = imageViewModel.DisplayedImage.PixelWidth;
                int height = imageViewModel.DisplayedImage.PixelHeight;
                int stride = imageViewModel.DisplayedImage.BackBufferStride;
                byte[] pixels = new byte[stride * height];
                imageViewModel.DisplayedImage.CopyPixels(pixels, stride, 0);

                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        int p = stride * y + x * 4;
                        pixels[p + 0] = (byte)(255 - pixels[p + 0]);
                        pixels[p + 1] = (byte)(255 - pixels[p + 1]);
                        pixels[p + 2] = (byte)(255 - pixels[p + 2]);
                    }
                }

                imageViewModel.DisplayedImage.WritePixels(new System.Windows.Int32Rect(0, 0, width, height), pixels, stride, 0);
            }
            else
            {
                var bitmapSource = new BitmapImage(new Uri(@"D:\Projects\GrainDetector\sample1.jpg"));
                imageViewModel.DisplayedImage = new WriteableBitmap(bitmapSource);
                imageViewModel.ZoomScale = 1.0;
                flag = true;
            }
        }
    }
}
