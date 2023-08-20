using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class ApplyImageProcessing : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private ImageDisplay imageDisplay;
        private ImageModification imageModification;

        public ApplyImageProcessing(
            AppData appData,
            ImageDisplay imageDisplay,
            ImageModification imageModification)
        {
            this.appData = appData;
            this.imageDisplay = imageDisplay;
            this.imageModification = imageModification;

            appData.PropertyChanged += appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.IsImageProcessingEnabled();
        }

        public void Execute(object parameter)
        {
            imageModification.ApplyImageProcessing();
            imageDisplay.RefreshRendering();
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
