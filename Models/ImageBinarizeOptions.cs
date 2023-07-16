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

        private int _threshold;
    }
}
