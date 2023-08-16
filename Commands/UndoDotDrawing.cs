using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class UndoDotDrawing : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private ImageDisplay imageDisplay;
        private IDotDrawing dotDrawing;

        public UndoDotDrawing(
            AppData appData,
            ImageDisplay imageDisplay,
            IDotDrawing dotDrawing)
        {
            this.appData = appData;
            this.imageDisplay = imageDisplay;
            this.dotDrawing = dotDrawing;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.DotDrawing;
        }

        public void Execute(object parameter)
        {
            dotDrawing.Undo();
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
