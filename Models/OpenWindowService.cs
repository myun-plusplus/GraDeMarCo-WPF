using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GraDeMarCoWPF.Models
{
    class OpenSubWindowService : IOpenWindowService
    {
        Window subwindow;

        public OpenSubWindowService(Window subWindow)
        {
            this.subwindow = subWindow;
        }

        public void OpenWindow()
        {
            subwindow.Show();
        }
    }
}
