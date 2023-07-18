using GraDeMarCoWPF.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace GraDeMarCoWPF.ViewModels
{
    public class ImageFilteringViewModel : ViewModelBase
    {
        private ImageFilterOptions imageFilterOptions;
        private IImageFiltering imageFiltering;

        public Dictionary<BlurOption, string> BlurOptionDictionary { get; private set; }
        public Dictionary<EdgeDetectOption, string> EdgeDetectOptionDictionary { get; private set; }

        public BlurOption BlurOption
        {
            get { return imageFilterOptions.BlurOption; }
            set
            {
                imageFilterOptions.BlurOption = value;
                UpdateImageModificatation.Execute(null);
                NotifyPropertyChanged(GetName.Of(() => BlurOption));
            }
        }

        public EdgeDetectOption EdgeDetectOption
        {
            get { return imageFilterOptions.EdgeDetectOption; }
            set
            {
                imageFilterOptions.EdgeDetectOption = value;
                UpdateImageModificatation.Execute(null);
                NotifyPropertyChanged(GetName.Of(() => EdgeDetectOption));
            }
        }

        public ICommand UpdateImageModificatation { get; private set; }

        public ImageFilteringViewModel(
            ImageFilterOptions options,
            ImageFiltering imageFiltering,
            ImageBinarizing imageBinarizing)
        {
            this.imageFilterOptions = options;
            this.imageFiltering = imageFiltering;

            BlurOptionDictionary = new Dictionary<BlurOption, string>();
            BlurOptionDictionary.Add(BlurOption.None, "なし");
            BlurOptionDictionary.Add(BlurOption.Gaussian, "ガウシアン");
            BlurOptionDictionary.Add(BlurOption.Gaussian3Times, "ガウシアン×3");
            BlurOptionDictionary.Add(BlurOption.Gaussian6Times, "ガウシアン×6");
            EdgeDetectOptionDictionary = new Dictionary<EdgeDetectOption, string>();
            EdgeDetectOptionDictionary.Add(EdgeDetectOption.None, "なし");
            EdgeDetectOptionDictionary.Add(EdgeDetectOption.Sobel, "ソーベル");
            EdgeDetectOptionDictionary.Add(EdgeDetectOption.Laplacian, "ラプラシアン");

            UpdateImageModificatation = CreateCommand(
                _ =>
                {
                    imageFiltering.FilterOriginalImage();
                    imageBinarizing.BinarizeFilteredImage();
                });
        }
    }
}
