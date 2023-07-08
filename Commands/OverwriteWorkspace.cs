using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using System.ComponentModel;
using System;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class OverwriteWorkspace : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;

        public OverwriteWorkspace(AppData appData)
        {
            this.appData = appData;
            appData.PropertyChanged += appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CanOverwriteWorkspace();
        }

        public void Execute(object parameter)
        {
            Workspace.Instance.Save(appData.WorkspacePath);
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
