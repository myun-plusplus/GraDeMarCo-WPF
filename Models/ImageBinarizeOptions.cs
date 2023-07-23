using GraDeMarCoWPF.Models;

namespace GraDeMarCoWPF.Models
{
    public class ImageBinarizeOptions : BindableBase
    {
        public int Threshold
        {
            get { return _threshold; }
            set
            {
                _threshold = value;
                NotifyPropertyChanged(GetName.Of(() => Threshold));

                System.Console.WriteLine(_threshold.ToString());
            }
        }

        public bool InvertsMonochrome
        {
            get { return _invertsMonochrome; }
            set
            {
                _invertsMonochrome = value;
                NotifyPropertyChanged(GetName.Of(() => InvertsMonochrome));

                System.Console.WriteLine(_invertsMonochrome.ToString());
            }
        }

        private int _threshold;
        private bool _invertsMonochrome;
    }
}
