using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class OpenImage : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private IWindowService imageWindowService;
        private IOpenFileDialogService openImageFileDialogService;

        public OpenImage(
            AppData appData,
            ImageData imageData,
            ImageDisplay imageDisplay,
            IWindowService imageWindowService,
            IOpenFileDialogService openImageFileDialogService)
        {
            this.appData = appData;
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageWindowService = imageWindowService;
            this.openImageFileDialogService = openImageFileDialogService;

            appData.PropertyChanged += appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CanOpenImage();
        }

        public void Execute(object parameter)
        {
            if (openImageFileDialogService.ShowDialog() ?? false)
            {
                appData.CurrentState = AppState.ImageOpened;
                appData.ImagePath = openImageFileDialogService.Filename;
                imageData.OpenImageFile(appData.ImagePath);
                imageDisplay.UpdateImage();
                imageWindowService.Open();
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
