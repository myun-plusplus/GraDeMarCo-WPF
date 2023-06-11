using GraDeMarCoWPF.Commands;
using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GraDeMarCoWPF.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public ICommand OpenImageWindowCommand { get; private set; }
        public ICommand CloseImageWindowCommand { get; private set; }

        private IOpenWindowService _openWindowService;

        public MainViewModel(ImageWindow imageWindow)
        {
            this.OpenImageWindowCommand = new OpenImageWindow(new OpenSubWindowService(imageWindow));
        }
    }
}
