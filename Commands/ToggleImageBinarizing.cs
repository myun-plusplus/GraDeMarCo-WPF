using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class ToggleImageBinarizing : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private IImageBinarizing imageBinarizing;

        public ToggleImageBinarizing(
            AppData appData,
            IImageBinarizing imageBinarizing)
        {
            this.appData = appData;
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
                imageBinarizing.StartFunction();
                imageBinarizing.BinarizeFilteredImage();
            }
            else
            {
                appData.CurrentState = AppState.ImageOpened;
                imageBinarizing.StopFunction();
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
