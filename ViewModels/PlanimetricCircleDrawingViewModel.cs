using GraDeMarCoWPF.Models;
using System.ComponentModel;
using System.Windows.Media;

namespace GraDeMarCoWPF.ViewModels
{
    public class PlanimetricCircleDrawingViewModel : ViewModelBase
    {
        private AppData appData;
        private PlanimetricCircle planimetricCircle;
        private OutlineDrawingTool planimetricCircleDrawingTool;
        private PlanimetricCircleDrawing planimetricCircleDrawing;

        public bool CircleDrawEnabled
        {
            get { return _circleDrawEnabled; }
            set
            {
                _circleDrawEnabled = value;
                NotifyPropertyChanged(GetName.Of(() => CircleDrawEnabled));

                if (value)
                {
                    appData.CurrentState = AppState.PlanimetricCircleDrawing;
                    planimetricCircleDrawing.StartFunction();
                }
                else
                {
                    appData.CurrentState = AppState.ImageOpened;
                    planimetricCircleDrawing.StopFunction();
                }
            }
        }

        public int LowerX
        {
            get { return planimetricCircle.LowerX; }
            set
            {
                planimetricCircle.LowerX = value;
                NotifyPropertyChanged(GetName.Of(() => LowerX));
            }
        }

        public int LowerY
        {
            get { return planimetricCircle.LowerY; }
            set
            {
                planimetricCircle.LowerY = value;
                NotifyPropertyChanged(GetName.Of(() => LowerY));
            }
        }

        public int Diameter
        {
            get { return planimetricCircle.Diameter; }
            set
            {
                planimetricCircle.Diameter = value;
                NotifyPropertyChanged(GetName.Of(() => Diameter));
            }
        }

        public Color Color
        {
            get { return planimetricCircleDrawingTool.Color; }
            set
            {
                planimetricCircleDrawingTool.Color = value;
                NotifyPropertyChanged(GetName.Of(() => Color));
            }
        }

        private bool _circleDrawEnabled;

        public PlanimetricCircleDrawingViewModel(
            AppData appData,
            PlanimetricCircle planimetricCircle,
            OutlineDrawingTool planimetricCircleDrawingTool,
            PlanimetricCircleDrawing planimetricCircleDrawing)
        {
            this.appData = Workspace.Instance.AppData;
            this.planimetricCircle = Workspace.Instance.PlanimetricCircle;
            this.planimetricCircleDrawingTool = Workspace.Instance.PlanimetricCircleDrawingTool;
            this.planimetricCircleDrawing = Workspace.Instance.PlanimetricCircleDrawing;

            planimetricCircle.PropertyChanged += imageArea_PropertyChanged;
            planimetricCircleDrawingTool.PropertyChanged += imageArea_PropertyChanged;

            planimetricCircleDrawingTool.Color = Colors.Blue;
        }

        private void imageArea_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }
    }
}
