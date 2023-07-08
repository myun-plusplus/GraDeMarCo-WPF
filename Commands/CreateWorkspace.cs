using GraDeMarCoWPF.Models;
using System;
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
    }
}
