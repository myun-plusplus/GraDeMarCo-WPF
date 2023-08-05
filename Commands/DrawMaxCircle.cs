using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class DrawMaxCircle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private IPlanimetricCircleDrawing planimetricCircleDrawing;

        public DrawMaxCircle(AppData appData, IPlanimetricCircleDrawing planimetricCircleDrawing)
        {
            this.appData = appData;
            this.planimetricCircleDrawing = planimetricCircleDrawing;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.PlanimetricCircleDrawing;
        }

        public void Execute(object parameter)
        {
            planimetricCircleDrawing.DrawMaxCircle();
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
