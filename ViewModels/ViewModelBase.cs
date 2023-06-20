using GraDeMarCoWPF.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        // コマンドの生成
        protected ICommand CreateCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            return new DelegateCommand(command, canExecute);
        }
    }
}
