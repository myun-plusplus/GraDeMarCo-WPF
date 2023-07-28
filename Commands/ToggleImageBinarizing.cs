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
        private IGrainDetecting grainDetecting;

        public ToggleImageBinarizing(
            AppData appData,
            IImageBinarizing imageBinarizing,
            IGrainDetecting grainDetecting)
        {
            this.appData = appData;
            this.imageBinarizing = imageBinarizing;
            this.grainDetecting = grainDetecting;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.ImageBinarizing;
        }

        public void Execute(object parameter)
        {
            if ((parameter as bool?) ?? false)
            {
                appData.CurrentState = AppState.ImageBinarizing;
                imageBinarizing.StartFunction();
                imageBinarizing.BinarizeFilteredImage();
                grainDetecting.DetectGrains();
            }
            else
            {
                appData.CurrentState = AppState.ImageOpened;
                imageBinarizing.StopFunction();
                imageBinarizing.BinarizeFilteredImage();
                grainDetecting.DetectGrains();
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
