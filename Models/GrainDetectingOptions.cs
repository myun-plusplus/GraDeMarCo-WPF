using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public class GrainDetectingOptions : BindableBase
    {
        public int MinimumGrainPixels
        {
            get { return _minimumGrainPixels; }
            set
            {
                _minimumGrainPixels = value;
                NotifyPropertyChanged(GetName.Of(() => MinimumGrainPixels));
            }
        }

        public bool DetectsGrainsInCircle
        {
            get { return _detectsGrainsInCircle; }
            set
            {
                _detectsGrainsInCircle = value;
                NotifyPropertyChanged(GetName.Of(() => DetectsGrainsInCircle));
            }
        }

        public bool DetectsGrainsOnCircle
        {
            get { return _detectsGrainsOnCircle; }
            set
            {
                _detectsGrainsOnCircle = value;
                NotifyPropertyChanged(GetName.Of(() => DetectsGrainsOnCircle));
            }
        }

        private int _minimumGrainPixels;
        private bool _detectsGrainsInCircle;
        private bool _detectsGrainsOnCircle;
    }
}
