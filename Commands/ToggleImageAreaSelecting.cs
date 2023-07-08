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
        private ImageAreaSelecting imageAreaSelecting;

        public ToggleImageAreaSelecting(
            AppData appData,
            ImageAreaSelecting imageAreaSelecting)
        {
            this.appData = appData;
            this.imageAreaSelecting = imageAreaSelecting;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            if ((parameter as bool?) ?? false)
            {
                return appData.CurrentState == AppState.ImageAreaSelecting;
            }
            else
            {
                return appData.CurrentState == AppState.ImageOpened;
            }
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
