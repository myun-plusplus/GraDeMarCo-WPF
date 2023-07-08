using GraDeMarCoWPF.Models;
using System;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class OpenWorkspace : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;

        public OpenWorkspace(AppData appData)
        {
            this.appData = appData;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CanOpenWorkspace();
        }

        public void Execute(object parameter)
        {
            appData.CurrentState = AppState.WorkspacePrepared;
            appData.WorkspacePath = @"D:\Projects\GrainDetector\sample1.dat";
            Workspace.Instance.Load(appData.WorkspacePath);
        }
    }
}
