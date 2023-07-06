using GraDeMarCoWPF.Models;
using System;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class DrawMaxCircle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private ImageArea imageArea;
        private PlanimetricCircle planimetricCircle;

        public DrawMaxCircle(AppData appData, ImageArea imageArea, PlanimetricCircle planimetricCircle)
        {
            this.appData = appData;
            this.imageArea = imageArea;
            this.planimetricCircle = planimetricCircle;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.PlanimetricCircleDrawing;
        }

        public void Execute(object parameter)
        {
            int centerX = (imageArea.LowerX + imageArea.UpperX) / 2;
            int centerY = (imageArea.LowerY + imageArea.UpperY) / 2;
            int radius = Math.Min(imageArea.UpperX - imageArea.LowerX, imageArea.UpperY - imageArea.LowerY) / 2;
            planimetricCircle.LowerX = centerX - radius;
            planimetricCircle.LowerY = centerY - radius;
            planimetricCircle.Diameter = 2 * radius;
        }
    }
}
