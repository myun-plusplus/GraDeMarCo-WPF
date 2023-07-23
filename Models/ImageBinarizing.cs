using GraDeMarCoWPF.Models.ImageProcessing;
using System.Windows;

namespace GraDeMarCoWPF.Models
{
    public class ImageBinarizing : IImageBinarizing
    {
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private ImageArea imageArea;
        private ImageBinarizeOptions imageBinarizeOptions;

        private bool isActive;

        public ImageBinarizing(
            ImageData imageData,
            ImageDisplay imageDisplay,
            ImageArea imageArea,
            ImageBinarizeOptions imageBinarizeOptions)
        {
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageArea = imageArea;
            this.imageBinarizeOptions = imageBinarizeOptions;
        }

        public void StartFunction()
        {
            isActive = true;
        }

        public void StopFunction()
        {
            isActive = false;
        }

        public void BinarizeFilteredImage()
        {
            int width = imageData.OriginalImage.PixelWidth;
            int height = imageData.OriginalImage.PixelHeight;
            int lowerX = imageArea.LowerX, upperX = imageArea.UpperX;
            int lowerY = imageArea.LowerY, upperY = imageArea.UpperY;
            int stride = imageData.OriginalImage.BackBufferStride;

            ImagePixels sourcePixels, destinationPixels, destinationPixels_vertical;
            {

                byte[] tmp = new byte[height * stride];
                imageData.FilteredImage.CopyPixels(tmp, stride, 0);
                sourcePixels = ImagePixels.ConvertFromOneDimArray(tmp, width, height, stride);
                destinationPixels = ImagePixels.ConvertFromOneDimArray(tmp, width, height, stride);
            }

            {
                byte[] tmp = ImagePixels.ConvertToOneDimArray(sourcePixels, width, height, stride);
                imageDisplay.DisplayedImage.Lock();
                imageDisplay.DisplayedImage.WritePixels(new Int32Rect(0, 0, width, height), tmp, stride, 0);
                imageDisplay.DisplayedImage.Unlock();
            }
        }
    }
}
