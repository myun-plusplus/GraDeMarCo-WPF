using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using System.ComponentModel;
using System;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class SaveImage : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private ImageIO imageIO;
        private ISaveFileDialogService saveImageDialogService;

        public SaveImage(
            AppData appData,
            ImageIO imageIO,
            ISaveFileDialogService saveImageDialogService)
        {
            this.appData = appData;
            this.imageIO = imageIO;
            this.saveImageDialogService = saveImageDialogService;

            appData.PropertyChanged += appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CanSaveImage();
        }

        public void Execute(object parameter)
        {
            if (saveImageDialogService.ShowDialog() ?? false)
            {
                imageIO.SaveDisplayedImage(saveImageDialogService.Filename);
            }
        }

        private void appData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }
    }
}
