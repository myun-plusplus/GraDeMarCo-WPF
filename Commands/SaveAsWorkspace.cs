using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class SaveAsWorkspace : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private ISaveFileDialogService saveFileDialogService;

        public SaveAsWorkspace(AppData appData, ISaveFileDialogService saveFileDialogService)
        {
            this.appData = appData;
            this.saveFileDialogService = saveFileDialogService;

            appData.PropertyChanged += appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CanSaveWorkspace();
        }

        public void Execute(object parameter)
        {
            if (saveFileDialogService.ShowDialog() ?? false)
            {
                appData.WorkspacePath = saveFileDialogService.Filename;
                Workspace.Instance.Save(appData.WorkspacePath);
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
