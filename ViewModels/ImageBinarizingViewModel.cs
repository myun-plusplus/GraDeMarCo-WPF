using GraDeMarCoWPF.Models;
using System.Collections.Generic;

namespace GraDeMarCoWPF.ViewModels
{
    public class ImageBinarizingViewModel : ViewModelBase
    {
        private ImageBinarizeOptions imageBinarizeOptions;
        private IImageBinarizing imageBinarizing;

        public int Threshold
        {
            get { return imageBinarizeOptions.Threshold; }
            set
            {
                imageBinarizeOptions.Threshold = value;
                imageBinarizing.BinarizeFilteredImage();
                NotifyPropertyChanged(GetName.Of(() => Threshold));
            }
        }

        public ImageBinarizingViewModel(
            ImageBinarizeOptions imageBinarizeOptions,
            IImageBinarizing imageBinarizing)
        {
            this.imageBinarizeOptions = imageBinarizeOptions;
            this.imageBinarizing = imageBinarizing;
        }
    }
}
