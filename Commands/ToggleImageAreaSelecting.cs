using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class ToggleImageAreaSelecting : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private IImageAreaSelecting imageAreaSelecting;

        public ToggleImageAreaSelecting(
            AppData appData,
            IImageAreaSelecting imageAreaSelecting)
        {
            this.appData = appData;
            this.imageAreaSelecting = imageAreaSelecting;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.ImageAreaSelecting;
        }

        public void Execute(object parameter)
        {
            if ((parameter as bool?) ?? false)
            {
                appData.CurrentState = AppState.ImageAreaSelecting;
                imageAreaSelecting.StartFunction();
            }
            else
            {
                appData.CurrentState = AppState.ImageOpened;
                imageAreaSelecting.StopFunction();
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
