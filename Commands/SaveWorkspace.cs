using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
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
            appData.PropertyChanged += appData_PropertyChanged;
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

        private void appData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }
    }
}
