using GraDeMarCoWPF.Models;
using System.ComponentModel;
using System.Windows.Media;

namespace GraDeMarCoWPF.ViewModels
{
    public class GrainDetectingViewModel : ViewModelBase
    {
        private AppData appData;
        private GrainDetectingOptions grainDetectingOptions;

        public int MinimumGrainPixels
        {
            get { return grainDetectingOptions.MinimumGrainPixels; }
            set
            {
                grainDetectingOptions.MinimumGrainPixels = value;
                NotifyPropertyChanged(GetName.Of(() => MinimumGrainPixels));
            }
        }

        public bool DetectsGrainsInCircle
        {
            get { return grainDetectingOptions.DetectsGrainsInCircle; }
            set
            {
                grainDetectingOptions.DetectsGrainsInCircle = value;
                NotifyPropertyChanged(GetName.Of(() => DetectsGrainsInCircle));
            }
        }

        public SolidColorBrush GrainInCircleDotColor
        {
            get { return new SolidColorBrush(grainDetectingOptions.GrainInCircleDotColor); }
        }

        public double GrainInCircleDotSize
        {
            get { return grainDetectingOptions.GrainInCircleDotSize; }
            set
            {
                grainDetectingOptions.GrainInCircleDotSize = value;
                NotifyPropertyChanged(GetName.Of(() => GrainInCircleDotSize));
            }
        }

        public bool DetectsGrainsOnCircle
        {
            get { return grainDetectingOptions.DetectsGrainsOnCircle; }
            set
            {
                grainDetectingOptions.DetectsGrainsOnCircle = value;
                NotifyPropertyChanged(GetName.Of(() => DetectsGrainsOnCircle));
            }
        }

        public SolidColorBrush GrainOnCircleDotColor
        {
            get { return new SolidColorBrush(grainDetectingOptions.GrainOnCircleDotColor); }
        }

        public double GrainOnCircleDotSize
        {
            get { return grainDetectingOptions.GrainOnCircleDotSize; }
            set
            {
                grainDetectingOptions.GrainOnCircleDotSize = value;
                NotifyPropertyChanged(GetName.Of(() => GrainOnCircleDotSize));
            }
        }

        public GrainDetectingViewModel(
            AppData appData,
            GrainDetectingOptions grainDetectingOptions)
        {
            this.appData = appData;
            this.grainDetectingOptions = grainDetectingOptions;

            grainDetectingOptions.PropertyChanged += grainDetectingOptions_PropertyChanged;
        }

        private void grainDetectingOptions_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }
    }
}
