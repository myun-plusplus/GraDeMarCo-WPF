using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class ClearDots : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private IDotDrawing dotDrawing;

        public ClearDots(
            AppData appData,
            IDotDrawing dotDrawing)
        {
            this.appData = appData;
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
            dotDrawing.Clear();
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
