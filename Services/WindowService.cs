using System.Windows;

namespace GraDeMarCoWPF.Services
{
    class WindowService : IWindowService
    {
        Window subWindow;

        public WindowService(Window subWindow)
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
