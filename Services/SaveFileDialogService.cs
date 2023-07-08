using Microsoft.Win32;

namespace GraDeMarCoWPF.Services
{
    internal class SaveFileDialogService : ISaveFileDialogService
    {
        private SaveFileDialog saveFileDialog;

        public SaveFileDialogService(string title, string filter)
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = title;
            saveFileDialog.Filter = filter;
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
        }

        public string Filename { get; set; }

        public bool? ShowDialog()
        {
            return saveFileDialog.ShowDialog();
        }
    }
}
