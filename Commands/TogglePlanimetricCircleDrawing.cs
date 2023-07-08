using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class TogglePlanimetricCircleDrawing : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private IPlanimetricCircleDrawing planimetricCircleDrawing;

        public TogglePlanimetricCircleDrawing(
            AppData appData,
            IPlanimetricCircleDrawing planimetricCircleDrawing)
        {
            this.appData = appData;
            this.planimetricCircleDrawing = planimetricCircleDrawing;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            if ((parameter as bool?) ?? false)
            {
                return appData.CurrentState == AppState.PlanimetricCircleDrawing;
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
                appData.CurrentState = AppState.PlanimetricCircleDrawing;
                planimetricCircleDrawing.StartFunction();
            }
            else
            {
                appData.CurrentState = AppState.ImageOpened;
                planimetricCircleDrawing.StopFunction();
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
