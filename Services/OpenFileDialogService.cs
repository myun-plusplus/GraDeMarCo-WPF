using Microsoft.Win32;

namespace GraDeMarCoWPF.Services
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        private OpenFileDialog openFileDialog;

        public OpenFileDialogService(string title, string filter)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.Filter = filter;
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
        }

        public string Filename { get; set; }

        public bool? ShowDialog()
        {
            return openFileDialog.ShowDialog();
        }
    }
}
