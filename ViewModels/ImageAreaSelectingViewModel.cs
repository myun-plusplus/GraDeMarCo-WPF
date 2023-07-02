using GraDeMarCoWPF.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.ViewModels
{
    public class ImageAreaSelectingViewModel : ViewModelBase
    {
        private AppData appData;
        private ImageArea imageArea;
        private ImageAreaSelecting imageAreaSelecting;

        public bool AreaReserveEnabled
        {
            get { return _areaSelectEnabled; }
            set
            {
                _areaSelectEnabled = value;
                NotifyPropertyChanged(GetName.Of(() => AreaReserveEnabled));

                if (value)
                {
                    appData.CurrentState = AppState.ImageAreaSelecting;
                    this.imageAreaSelecting.StartFunction();
                }
                else
                {
                    appData.CurrentState = AppState.ImageOpened;
                    this.imageAreaSelecting.StopFunction();
                }
            }
        }

        public int LowerX
        {
            get { return imageArea.LowerX; }
            set
            {
                imageArea.LowerX = value;
                NotifyPropertyChanged(GetName.Of(() => LowerX));
            }
        }

        public int UpperX
        {
            get { return imageArea.UpperX; }
            set
            {
                imageArea.UpperX = value;
                NotifyPropertyChanged(GetName.Of(() => UpperX));
            }
        }
        public int LowerY
        {
            get { return imageArea.LowerY; }
            set
            {
                imageArea.LowerY = value;
                NotifyPropertyChanged(GetName.Of(() => LowerY));
            }
        }

        public int UpperY
        {
            get { return imageArea.UpperY; }
            set
            {
                imageArea.UpperY = value;
                NotifyPropertyChanged(GetName.Of(() => UpperY));
            }
        }

        public ICommand SelectMaxAreaCommand { get; private set; }

        public ImageAreaSelectingViewModel()
        {
            this.appData = Workspace.Instance.AppData;
            this.imageArea = Workspace.Instance.ImageArea;
            this.imageAreaSelecting = Workspace.Instance.ImageAreaSelecting;

            imageArea.PropertyChanged += imageArea_PropertyChanged;
        }

        private void imageArea_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        private bool _areaSelectEnabled;
    }
}
