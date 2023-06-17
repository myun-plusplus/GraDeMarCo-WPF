﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GraDeMarCoWPF.ViewModels
{
    /// <summary>
    /// ViewModelの基底クラス
    /// INotifyPropertyChanged と IDataErrorInfo を実装する
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        // INotifyPropertyChanged.PropertyChanged の実装
        public event PropertyChangedEventHandler PropertyChanged;

        // INotifyPropertyChanged.PropertyChangedイベントを発生させる。
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // IDataErrorInfo用のエラーメッセージを保持する辞書
        private Dictionary<string, string> _ErrorMessages = new Dictionary<string, string>();

        // IDataErrorInfo.Error の実装
        string IDataErrorInfo.Error
        {
            get { return (_ErrorMessages.Count > 0) ? "Has Error" : null; }
        }

        // IDataErrorInfo.Item の実装
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (_ErrorMessages.ContainsKey(columnName))
                {
                    return _ErrorMessages[columnName];
                }
                else
                {
                    return null;
                }
            }
        }

        // エラーメッセージのセット
        protected void SetError(string propertyName, string errorMessage)
        {
            _ErrorMessages[propertyName] = errorMessage;
        }

        // エラーメッセージのクリア
        protected void ClearErrror(string propertyName)
        {
            if (_ErrorMessages.ContainsKey(propertyName))
            {
                _ErrorMessages.Remove(propertyName);
            }
        }

        // ICommand実装用のヘルパークラス
        private class _DelegateCommand : ICommand
        {
            private Action<object> _Command;        // コマンド本体
            private Func<object, bool> _CanExecute;  // 実行可否

            // コンストラクタ
            public _DelegateCommand(Action<object> command, Func<object, bool> canExecute = null)
            {
                if (command == null)
                {
                    throw new ArgumentNullException();
                }

                _Command = command;
                _CanExecute = canExecute;
            }

            // ICommand.Executeの実装
            void ICommand.Execute(object parameter)
            {
                _Command(parameter);
            }

            // ICommand.Executeの実装
            bool ICommand.CanExecute(object parameter)
            {
                if (_CanExecute != null)
                {
                    return _CanExecute(parameter);
                }
                else
                {
                    return true;
                }
            }

            // ICommand.CanExecuteChanged の実装
            event EventHandler ICommand.CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }

        // コマンドの生成
        protected ICommand CreateCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            return new _DelegateCommand(command, canExecute);
        }
    }
}
