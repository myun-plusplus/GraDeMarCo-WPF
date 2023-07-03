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
        private IWindowService _service;

        public CloseImageWindow(IWindowService imageWindowOpen)
        {
            _service = imageWindowOpen;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _service.Close();
        }
    }
}
