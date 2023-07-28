using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class ToggleGrainDetecting : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private ImageDisplay imageDisplay;
        private IGrainDetecting grainDetecting;

        public ToggleGrainDetecting(
            AppData appData,
            ImageDisplay imageDisplay,
            IGrainDetecting grainDetecting)
        {
            this.appData = appData;
            this.imageDisplay = imageDisplay;
            this.grainDetecting = grainDetecting;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.GrainDetecting;
        }

        public void Execute(object parameter)
        {
            if ((parameter as bool?) ?? false)
            {
                appData.CurrentState = AppState.GrainDetecting;
                grainDetecting.StartFunction();
                grainDetecting.DetectGrains();
                imageDisplay.RefreshRendering();
            }
            else
            {
                appData.CurrentState = AppState.ImageOpened;
                grainDetecting.StopFunction();
                grainDetecting.DetectGrains();
                imageDisplay.RefreshRendering();
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
