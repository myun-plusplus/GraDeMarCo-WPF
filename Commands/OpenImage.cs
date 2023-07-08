using System;
using System.ComponentModel;
using System.Windows.Input;
using GraDeMarCoWPF.Models;

namespace GraDeMarCoWPF.Commands
{
    public class OpenImage : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private IWindowService imageWindowService;

        public OpenImage(
            AppData appData,
            ImageData imageData,
            ImageDisplay imageDisplay,
            IWindowService imageWindowService)
        {
            this.appData = appData;
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageWindowService = imageWindowService;

            appData.PropertyChanged += appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CanOpenImage();
        }

        public void Execute(object parameter)
        {
            appData.CurrentState = AppState.ImageOpened;
            imageData.OpenImageFile(null);
            imageDisplay.UpdateImage();
            imageWindowService.Open();
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
