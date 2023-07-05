using GraDeMarCoWPF.Models;
using System;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class SelectColor : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private OutlineDrawingTool drawingTool;
        private IColorDialogService colorDialogService;

        public SelectColor(
            OutlineDrawingTool drawingTool,
            IColorDialogService colorDialogService)
        {
            this.drawingTool = drawingTool;
            this.colorDialogService = colorDialogService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            bool? dialogResult = colorDialogService.ShowDialog();
            if (dialogResult ?? false)
            {
                drawingTool.Color = colorDialogService.Color;
            }
        }
    }
}
