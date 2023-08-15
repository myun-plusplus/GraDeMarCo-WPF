using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using System.ComponentModel;
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

        public DotDrawingViewModel(
            IColorDialogService colorDialogService,
            AppData appData,
            DotDrawingTool dotDrawingTool,
            DotData detectedDotData,
            DotData drawnDotData)
        {
            this.appData = Workspace.Instance.AppData;
            this.dotDrawingTool = dotDrawingTool;

            dotDrawingTool.PropertyChanged += dotDrawingTool_PropertyChanged;
        }

        private void dotDrawingTool_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        private bool _dotDrawEnabled;
    }
}
