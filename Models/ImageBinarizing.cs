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
            if (!isActive)
            {
                imageData.BinarizedImage = imageData.FilteredImage.Clone();
                imageDisplay.DisplayedImage = imageData.FilteredImage.Clone();
                return;
            }

            int width = imageData.OriginalImage.PixelWidth;
            int height = imageData.OriginalImage.PixelHeight;
            int lowerX = imageArea.LowerX, upperX = imageArea.UpperX;
            int lowerY = imageArea.LowerY, upperY = imageArea.UpperY;
            int stride = imageData.OriginalImage.BackBufferStride;

            byte[] pixels = new byte[height * stride];
            imageData.FilteredImage.CopyPixels(pixels, stride, 0);

            const int RedFactor = (int)(0.298912 * 1024);
            const int GreenFactor = (int)(0.586611 * 1024);
            const int BlueFactor = (int)(0.114478 * 1024);

            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    int position = y * stride + x * Constants.ColorCount;
                    byte b = pixels[position + 0];
                    byte g = pixels[position + 1];
                    byte r = pixels[position + 2];
                    int value = (r * RedFactor + g * GreenFactor + b * BlueFactor) >> 10;
                    if ((value < imageBinarizeOptions.Threshold) ^ imageBinarizeOptions.InvertsMonochrome)
                    {
                        pixels[position + 0] = 0;
                        pixels[position + 1] = 0;
                        pixels[position + 2] = 0;
                    }
                    else
                    {
                        pixels[position + 0] = 255;
                        pixels[position + 1] = 255;
                        pixels[position + 2] = 255;
                    }
                }
            }

            imageDisplay.DisplayedImage.Lock();
            imageDisplay.DisplayedImage.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            imageDisplay.DisplayedImage.Unlock();
        }
    }
}
