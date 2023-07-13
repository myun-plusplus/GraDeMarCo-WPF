using System.Xml.Linq;

namespace GraDeMarCoWPF.Models
{
    public enum BlurOption
    {
        None,
        Gaussian,
        Gaussian3Times,
        Gaussian6Times
    }

    public enum EdgeDetectOption
    {
        None,
        Sobel,
        Laplacian
    }

    public class ImageFilterOptions : BindableBase
    {
        public BlurOption BlurOption
        {
            get { return _blurOption; }
            set
            {
                _blurOption = value;
                NotifyPropertyChanged(GetName.Of(() => BlurOption));
            }
        }

        public EdgeDetectOption EdgeDetectOption
        {
            get { return _edgeDetectOption; }
            set
            {
                _edgeDetectOption = value;
                NotifyPropertyChanged(GetName.Of(() => EdgeDetectOption));
            }
        }

        private BlurOption _blurOption;
        private EdgeDetectOption _edgeDetectOption;
    }
}
