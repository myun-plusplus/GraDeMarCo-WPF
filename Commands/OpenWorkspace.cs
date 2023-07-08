using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class OpenWorkspace : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        IOpenFileDialogService openFileDialogService;

        public OpenWorkspace(AppData appData, IOpenFileDialogService openFileDialogService)
        {
            this.appData = appData;
            this.openFileDialogService = openFileDialogService;

            appData.PropertyChanged += appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CanOpenWorkspace();
        }

        public void Execute(object parameter)
        {
            if (openFileDialogService.ShowDialog() ?? false)
            {
                appData.CurrentState = AppState.WorkspacePrepared;
                appData.WorkspacePath = openFileDialogService.Filename;
                Workspace.Instance.Load(appData.WorkspacePath);
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
