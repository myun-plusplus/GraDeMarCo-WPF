using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class ToggleDotDrawing : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private ImageDisplay imageDisplay;

        public ToggleDotDrawing(
            AppData appData,
            ImageDisplay imageDisplay)
        {
            this.appData = appData;
            this.imageDisplay = imageDisplay;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.DotDrawing;
        }

        public void Execute(object parameter)
        {
            if ((parameter as bool?) ?? false)
            {
                appData.CurrentState = AppState.DotDrawing;
                imageDisplay.RefreshRendering();
            }
            else
            {
                appData.CurrentState = AppState.ImageOpened;
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
