using GraDeMarCoWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class CloseImageWindow : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IOpenWindowService _service;

        public CloseImageWindow(IOpenWindowService imageWindowOpen)
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
