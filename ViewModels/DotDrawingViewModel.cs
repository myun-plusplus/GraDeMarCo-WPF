using GraDeMarCoWPF.Commands;
using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace GraDeMarCoWPF.ViewModels
{
    public class DotDrawingViewModel : ViewModelBase
    {
        private AppData appData;
        private DotDrawingTool dotDrawingTool;

        public bool DotDrawEnabled
        {
            get { return _dotDrawEnabled; }
            set
            {
                _dotDrawEnabled = value;
                NotifyPropertyChanged(GetName.Of(() => DotDrawEnabled));
            }
        }

        public SolidColorBrush Color
        {
            get { return new SolidColorBrush(dotDrawingTool.Color); }
        }

        public double Size
        {
            get { return dotDrawingTool.Size; }
            set
            {
                dotDrawingTool.Size = value;
                NotifyPropertyChanged(GetName.Of(() => Size));
            }
        }

        public ICommand ToggleDotDrawing { get; private set; }
        public ICommand SelectDrawnDotColor { get; private set; }
        public ICommand UndoDotDrawing { get; private set; }
        public ICommand RedoDotDrawing { get; private set; }
        public ICommand ClearDots { get; private set; }

        public DotDrawingViewModel(
            IColorDialogService colorDialogService,
            AppData appData,
            ImageDisplay imageDisplay,
            DotDrawingTool dotDrawingTool,
            DotData detectedDotData,
            DotData drawnDotData,
            DotDrawing dotDrawing)
        {
            this.appData = Workspace.Instance.AppData;
            this.dotDrawingTool = dotDrawingTool;

            ToggleDotDrawing = new ToggleDotDrawing(appData, imageDisplay);
            SelectDrawnDotColor = new SelectDrawnDotColor(appData, dotDrawingTool, colorDialogService);
            UndoDotDrawing = new UndoDotDrawing(appData, dotDrawing);
            RedoDotDrawing = new RedoDotDrawing(appData, dotDrawing);
            ClearDots = new ClearDots(appData, dotDrawing);

            dotDrawingTool.PropertyChanged += dotDrawingTool_PropertyChanged;
        }

        private void dotDrawingTool_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        private bool _dotDrawEnabled;
    }
}
