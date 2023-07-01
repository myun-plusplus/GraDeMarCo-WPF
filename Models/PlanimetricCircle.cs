using System;

namespace GraDeMarCoWPF.Models
{
    [Serializable]
    public class PlanimetricCircle : BindableBase
    {
        public int LowerX
        {
            get { return _lowerX; }
            set
            {
                _lowerX = value;
                NotifyPropertyChanged(GetName.Of(() => LowerX));
            }
        }
        public int LowerY
        {
            get { return _lowerY; }
            set
            {
                _lowerY = value;
                NotifyPropertyChanged(GetName.Of(() => LowerY));
            }
        }

        public int Diameter
        {
            get { return _diameter; }
            set
            {
                _diameter = value;
                NotifyPropertyChanged(GetName.Of(() => Diameter));
            }
        }

        private int _lowerX;
        private int _lowerY;
        private int _diameter;
    }
}
