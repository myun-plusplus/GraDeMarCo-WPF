using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class SelectDrawnDotColor : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private DotDrawingTool drawingTool;
        private IColorDialogService colorDialogService;

        public SelectDrawnDotColor(
            AppData appData,
            DotDrawingTool drawingTool,
            IColorDialogService colorDialogService)
        {
            this.appData = appData;
            this.drawingTool = drawingTool;
            this.colorDialogService = colorDialogService;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.DotDrawing;
        }

        public void Execute(object parameter)
        {
            bool? dialogResult = colorDialogService.ShowDialog();
            if (dialogResult ?? false)
            {
                drawingTool.Color = colorDialogService.Color;
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
