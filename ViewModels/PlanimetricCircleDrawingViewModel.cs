using GraDeMarCoWPF.Commands;
using GraDeMarCoWPF.Models;
using System.ComponentModel;
using System.Windows.Input;
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
                }
                else
                {
                    appData.CurrentState = AppState.ImageOpened;
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

        public SolidColorBrush Color
        {
            get { return new SolidColorBrush(planimetricCircleDrawingTool.Color); }
        }

        public ICommand TogglePlanimetricCircleDrawing { get; private set; }
        public ICommand DrawMaxCircle { get; private set; }
        public ICommand SelectColor { get; private set; }

        private bool _circleDrawEnabled;

        public PlanimetricCircleDrawingViewModel(
            IColorDialogService colorDialogService,
            AppData appData,
            ImageArea imageArea,
            PlanimetricCircle planimetricCircle,
            OutlineDrawingTool planimetricCircleDrawingTool,
            PlanimetricCircleDrawing planimetricCircleDrawing)
        {
            this.appData = Workspace.Instance.AppData;
            this.planimetricCircle = Workspace.Instance.PlanimetricCircle;
            this.planimetricCircleDrawingTool = Workspace.Instance.PlanimetricCircleDrawingTool;
            this.planimetricCircleDrawing = Workspace.Instance.PlanimetricCircleDrawing;

            TogglePlanimetricCircleDrawing = new TogglePlanimetricCircleDrawing(appData, planimetricCircleDrawing);
            SelectColor = new SelectColor(appData, planimetricCircleDrawingTool, colorDialogService);
            DrawMaxCircle = new DrawMaxCircle(appData, imageArea, planimetricCircle);

            planimetricCircle.PropertyChanged += imageArea_PropertyChanged;
            planimetricCircleDrawingTool.PropertyChanged += imageArea_PropertyChanged;
        }

        private void imageArea_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }
    }
}
