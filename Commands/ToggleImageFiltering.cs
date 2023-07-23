using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class ToggleImageFiltering : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private IImageFiltering imageFiltering;
        private IImageBinarizing imageBinarizing;

        public ToggleImageFiltering(
            AppData appData,
            IImageFiltering imageFiltering,
            IImageBinarizing imageBinarizing)
        {
            this.appData = appData;
            this.imageFiltering = imageFiltering;
            this.imageBinarizing = imageBinarizing;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.ImageFiltering;
        }

        public void Execute(object parameter)
        {
            if ((parameter as bool?) ?? false)
            {
                appData.CurrentState = AppState.ImageFiltering;
                imageFiltering.StartFunction();
                imageFiltering.FilterOriginalImage();
                imageBinarizing.BinarizeFilteredImage();
            }
            else
            {
                appData.CurrentState = AppState.ImageOpened;
                imageFiltering.StopFunction();
                imageFiltering.FilterOriginalImage();
                imageBinarizing.BinarizeFilteredImage();
            }
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
