using System;

namespace GraDeMarCoWPF.Models
{
    public enum AppState
    {
        NoData,
        Inactive,
        ImageAreaSelecting
    }

    [Serializable]
    public class AppData
    {
        public AppState CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
            }
        }

        public string WorkspacePath
        {
            get { return _workspacePath; }
            set
            {
                _workspacePath = value;
            }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
            }
        }

        [NonSerialized]
        private AppState _currentState;
        [NonSerialized]
        private string _workspacePath;
        private string _imagePath;
    }
}
