using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class SelectMaxArea : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        ImageData imageData;
        private ImageArea imageArea;

        public SelectMaxArea(AppData appData, ImageData imageData, ImageArea imageArea)
        {
            this.appData = appData;
            this.imageData = imageData;
            this.imageArea = imageArea;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.ImageAreaSelecting;
        }

        public void Execute(object parameter)
        {
            imageArea.LowerX = 0;
            imageArea.UpperX = (int)imageData.OriginalImage.PixelWidth - 1;
            imageArea.LowerY = 0;
            imageArea.UpperY = (int)imageData.OriginalImage.PixelHeight - 1;
        }

        private void appData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }
    }
}
