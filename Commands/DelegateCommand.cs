using System;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action<object> command;        // コマンド本体
        private Func<object, bool> canExecute;  // 実行可否

        public DelegateCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }

            this.command = command;
            this.canExecute = canExecute;
        }

        void ICommand.Execute(object parameter)
        {
            command(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }
            else
            {
                return true;
            }
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
