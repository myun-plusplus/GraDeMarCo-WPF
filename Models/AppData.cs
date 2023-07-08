using System;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public enum AppState
    {
        None,
        WorkspacePrepared,
        ImageOpened,
        ImageAreaSelecting,
        PlanimetricCircleDrawing
    }

    [Serializable]
    public class AppData : BindableBase
    {
        public AppState CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
                NotifyPropertyChanged(GetName.Of(() => CurrentState));
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

        public bool CanCreateWorkspace()
        {
            return CurrentState == AppState.None ||
                CurrentState == AppState.WorkspacePrepared ||
                CurrentState == AppState.ImageOpened;
        }

        public bool CanOpenWorkspace()
        {
            return CurrentState == AppState.None ||
                CurrentState == AppState.WorkspacePrepared ||
                CurrentState == AppState.ImageOpened;
        }

        public bool CanSaveWorkspace()
        {
            return CurrentState == AppState.WorkspacePrepared || CurrentState == AppState.ImageOpened;
        }

        public bool CanOpenImage()
        {
            return CurrentState == AppState.WorkspacePrepared || CurrentState == AppState.ImageOpened;
        }

        public bool CanZoomInOut()
        {
            return CurrentState != AppState.None && CurrentState != AppState.WorkspacePrepared;
        }

        public bool IsClickEnabled()
        {
            return CurrentState == AppState.ImageAreaSelecting;
        }

        public bool IsMouseMoveEnabled()
        {
            return CurrentState == AppState.ImageAreaSelecting;
        }

        [NonSerialized]
        private AppState _currentState;
        [NonSerialized]
        private string _workspacePath;
        private string _imagePath;
    }
}
