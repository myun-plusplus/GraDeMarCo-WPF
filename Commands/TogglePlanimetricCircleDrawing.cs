using GraDeMarCoWPF.Models;
using System;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class TogglePlanimetricCircleDrawing : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private PlanimetricCircleDrawing planimetricCircleDrawing;

        public TogglePlanimetricCircleDrawing(
            AppData appData,
            PlanimetricCircleDrawing planimetricCircleDrawing)
        {
            this.appData = appData;
            this.planimetricCircleDrawing = planimetricCircleDrawing;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState != AppState.None &&
                appData.CurrentState != AppState.WorkspacePrepared;
        }

        public void Execute(object parameter)
        {
            if ((parameter as bool?) ?? false)
            {
                planimetricCircleDrawing.StartFunction();
            }
            else
            {
                planimetricCircleDrawing.StopFunction();
            }
        }
    }
}
