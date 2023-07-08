using System.Windows;

namespace GraDeMarCoWPF.Models
{
    class SubWindowService : IWindowService
    {
        Window subWindow;

        public SubWindowService(Window subWindow)
        {
            this.subWindow = subWindow;
        }

        public void Open()
        {
            subWindow.Show();
        }

        public void Close()
        {
            subWindow.Hide();
        }
    }
}
