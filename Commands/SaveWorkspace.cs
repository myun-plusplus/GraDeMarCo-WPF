using GraDeMarCoWPF.Models;
using System;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class SaveWorkspace : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;

        public SaveWorkspace(AppData appData)
        {
            this.appData = appData;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CanSaveWorkspace();
        }

        public void Execute(object parameter)
        {
            string path = @"D:\Projects\GrainDetector\sample1.dat";
            Workspace.Instance.Save(path);
        }
    }
}
