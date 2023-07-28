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
        private IGrainDetecting grainDetecting;

        public ToggleGrainDetecting(
            AppData appData,
            IGrainDetecting grainDetecting)
        {
            this.appData = appData;
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
            }
            else
            {
                appData.CurrentState = AppState.ImageOpened;
                grainDetecting.StopFunction();
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
