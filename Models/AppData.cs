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
    public class AppData
    {
        private ImageAreaSelecting imageAreaSelecting;
        private PlanimetricCircleDrawing planetricCircleDrawing;

        public AppData(
            ImageAreaSelecting imageAreaSelecting,
            PlanimetricCircleDrawing planetricCircleDrawing)
        {
            this.imageAreaSelecting = imageAreaSelecting;
            this.planetricCircleDrawing = planetricCircleDrawing;
        }

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
            return CurrentState == AppState.None || CurrentState == AppState.WorkspacePrepared;
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

        public void DrawOnRender(DrawingContext drawingContext)
        {
            switch (CurrentState)
            {
                case AppState.None:
                    break;
                case AppState.WorkspacePrepared:
                    break;
                case AppState.ImageOpened:
                    break;
                case AppState.ImageAreaSelecting:
                    imageAreaSelecting.DrawOnRender(drawingContext);
                    break;
                case AppState.PlanimetricCircleDrawing:
                    planetricCircleDrawing.DrawOnRender(drawingContext);
                    break;
            }
        }

        [NonSerialized]
        private AppState _currentState;
        [NonSerialized]
        private string _workspacePath;
        private string _imagePath;
    }
}
