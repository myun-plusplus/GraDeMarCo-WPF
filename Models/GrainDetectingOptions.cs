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

        public Color GrainInCircleDotColor
        {
            get { return _grainInCircleDotColor; }
            set
            {
                _grainInCircleDotColor = value;
                NotifyPropertyChanged(GetName.Of(() => GrainInCircleDotColor));
            }
        }

        public double GrainInCircleDotSize
        {
            get { return _grainInCircleDotSize; }
            set
            {
                _grainInCircleDotSize = value;
                NotifyPropertyChanged(GetName.Of(() => GrainInCircleDotSize));
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

        public Color GrainOnCircleDotColor
        {
            get { return _grainOnCircleDotColor; }
            set
            {
                _grainOnCircleDotColor = value;
                NotifyPropertyChanged(GetName.Of(() => GrainOnCircleDotColor));
            }
        }

        public double GrainOnCircleDotSize
        {
            get { return _grainOnCircleDotSize; }
            set
            {
                _grainOnCircleDotSize = value;
                NotifyPropertyChanged(GetName.Of(() => GrainOnCircleDotSize));
            }
        }

        private int _minimumGrainPixels;
        private bool _detectsGrainsInCircle;
        private Color _grainInCircleDotColor;
        private double _grainInCircleDotSize;
        private bool _detectsGrainsOnCircle;
        private Color _grainOnCircleDotColor;
        private double _grainOnCircleDotSize;
    }
}
