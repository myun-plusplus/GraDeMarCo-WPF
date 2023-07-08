using GraDeMarCoWPF.Commands;
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

        public ICommand ToggleImageAreaSelecting { get; private set; }
        public ICommand SelectMaxArea { get; private set; }

        public ImageAreaSelectingViewModel(
            AppData appData,
            ImageData imageData,
            ImageArea imageArea,
            ImageAreaSelecting imageAreaSelecting)
        {
            this.appData = appData;
            this.imageArea = imageArea;
            this.imageAreaSelecting = imageAreaSelecting;

            ToggleImageAreaSelecting = new ToggleImageAreaSelecting(appData, imageAreaSelecting);
            SelectMaxArea = new SelectMaxArea(appData, imageData, imageArea);

            imageArea.PropertyChanged += imageArea_PropertyChanged;
        }

        private void imageArea_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        private bool _areaSelectEnabled;
    }
}
