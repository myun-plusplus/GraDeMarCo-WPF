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
        private ImageArea imageArea;
        private PlanimetricCircle planimetricCircle;

        public DrawMaxCircle(AppData appData, ImageArea imageArea, PlanimetricCircle planimetricCircle)
        {
            this.appData = appData;
            this.imageArea = imageArea;
            this.planimetricCircle = planimetricCircle;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.PlanimetricCircleDrawing;
        }

        public void Execute(object parameter)
        {
            double centerX = (imageArea.LowerX + imageArea.UpperX) / 2.0;
            double centerY = (imageArea.LowerY + imageArea.UpperY) / 2.0;
            int diameter = Math.Min(imageArea.UpperX - imageArea.LowerX, imageArea.UpperY - imageArea.LowerY);
            planimetricCircle.LowerX = (int)(centerX - diameter / 2.0);
            planimetricCircle.LowerY = (int)(centerY - diameter / 2.0);
            planimetricCircle.Diameter = (int)diameter;
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
