using System;
using System.Windows.Input;
using GraDeMarCoWPF.Models;

namespace GraDeMarCoWPF.Commands
{
    public class OpenImageWindow : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IOpenWindowService _service;

        public OpenImageWindow(IOpenWindowService imageWindowOpen)
        {
            _service = imageWindowOpen;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _service.OpenWindow();
        }
    }
}
