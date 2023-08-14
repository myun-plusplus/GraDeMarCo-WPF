using GraDeMarCoWPF.Commands;
using GraDeMarCoWPF.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace GraDeMarCoWPF.ViewModels
{
    public class ImageBinarizingViewModel : ViewModelBase
    {
        private ImageBinarizeOptions imageBinarizeOptions;

        public int Threshold
        {
            get { return imageBinarizeOptions.Threshold; }
            set
            {
                imageBinarizeOptions.Threshold = value;
                UpdateImageBinarizing.Execute(null);
                NotifyPropertyChanged(GetName.Of(() => Threshold));
            }
        }

        public bool InvertsMonochrome
        {
            get { return imageBinarizeOptions.InvertsMonochrome; }
            set
            {
                imageBinarizeOptions.InvertsMonochrome = value;
                UpdateImageBinarizing.Execute(null);
                NotifyPropertyChanged(GetName.Of(() => InvertsMonochrome));
            }
        }

        public ICommand ToggleImageBinarizing { get; private set; }
        public ICommand UpdateImageBinarizing { get; private set; }

        public ImageBinarizingViewModel(
            AppData appData,
            ImageDisplay imageDisplay,
            ImageBinarizeOptions imageBinarizeOptions,
            IImageBinarizing imageBinarizing,
            IGrainDetecting grainDetecting)
        {
            this.imageBinarizeOptions = imageBinarizeOptions;

            ToggleImageBinarizing = new ToggleImageBinarizing(appData, imageBinarizing, grainDetecting);
            UpdateImageBinarizing = CreateCommand(
                _ =>
                {
                    imageBinarizing.BinarizeFilteredImage();
                    grainDetecting.DetectGrains();
                    imageDisplay.RefreshRendering();
                });
        }
    }
}
