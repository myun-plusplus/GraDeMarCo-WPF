using System.Windows;

namespace GraDeMarCoWPF.Models
{
    public class ImageArea : BindableBase
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

        public int UpperX
        {
            get { return _upperX; }
            set
            {
                _upperX = value;
                NotifyPropertyChanged(GetName.Of(() => UpperX));
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

        public int UpperY
        {
            get { return _upperY; }
            set
            {
                _upperY = value;
                NotifyPropertyChanged(GetName.Of(() => UpperY));
            }
        }

        public Rect Area
        {
            get
            {
                return new Rect(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY);
            }
        }

        private int _lowerX;
        private int _upperX;
        private int _lowerY;
        private int _upperY;
    }
}
