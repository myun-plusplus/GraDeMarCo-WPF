using GraDeMarCo;
using GraDeMarCoWPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GraDeMarCoWPF.ViewModels
{
    public class ImageViewModel : ViewModelBase
    {
        public WriteableBitmap DisplayedImage
        {
            get
            {
                return _displayedImage;
            }
            set
            {
                _displayedImage = value;
                NotifyPropertyChanged(GetName.Of(() => DisplayedImage));
            }
        }

        public double ZoomScale
        {
            get
            {
                return _zoomScale;
            }
            set
            {
                _zoomScale = value;
                NotifyPropertyChanged(GetName.Of(() => ZoomScale));
            }
        }

        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }

        public ICommand TestClickCommand { get; private set; }

        private WriteableBitmap _displayedImage;
        private double _zoomScale;

        private ImageData imageData;

        public ImageViewModel(/*ImageData imageData*/)
        {
            //this.imageData = imageData;
            this.TestClickCommand = new TestClick(this);

            //this.ZoomInCommand = CreateCommand(_ => { ZoomScale *= 2.0; }, _ => ZoomScale < 8.0);
            //this.ZoomOutCommand = CreateCommand(_ => { ZoomScale *= 0.5; }, _ => ZoomScale > 0.125);
        }
    }
}
