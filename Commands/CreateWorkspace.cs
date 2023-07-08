using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class CreateWorkspace : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;

        public CreateWorkspace(AppData appData)
        {
            this.appData = appData;
            appData.PropertyChanged += appData_PropertyChanged;
        }   

        public bool CanExecute(object parameter)
        {
            return appData.CanCreateWorkspace();
        }

        public void Execute(object parameter)
        {
            appData.CurrentState = AppState.WorkspacePrepared;
            Workspace.Instance.Initialize();
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
