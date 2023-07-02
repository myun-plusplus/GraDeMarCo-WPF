using GraDeMarCoWPF.Models;
using System.ComponentModel;
using System.Windows.Media;

namespace GraDeMarCoWPF.ViewModels
{
    public class PlanimetricCircleDrawingViewModel : ViewModelBase
    {
        public bool CircleDrawEnabled
        {
            get { return _circleDrawEnabled; }
            set
            {
                _circleDrawEnabled = value;
                NotifyPropertyChanged(GetName.Of(() => CircleDrawEnabled));

                if (value)
                {
                    planimetricCircleDrawing.StartFunction();
                }
                else
                {
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

        private PlanimetricCircle planimetricCircle;
        private OutlineDrawingTool planimetricCircleDrawingTool;
        private PlanimetricCircleDrawing planimetricCircleDrawing;

        private bool _circleDrawEnabled;

        public PlanimetricCircleDrawingViewModel()
        {
            this.planimetricCircle = Workspace.Instance.PlanimetricCircle;
            this.planimetricCircleDrawingTool = Workspace.Instance.PlanimetricCircleDrawingTool;
            this.planimetricCircleDrawing = Workspace.Instance.PlanimetricCircleDrawing;

            planimetricCircle.PropertyChanged += imageArea_PropertyChanged;
            planimetricCircleDrawingTool.PropertyChanged += imageArea_PropertyChanged;
        }

        private void imageArea_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }
    }
}
