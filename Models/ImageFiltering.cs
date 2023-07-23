using System.Threading.Tasks;
using System;
using System.Windows.Media;
using GraDeMarCoWPF.Models.ImageProcessing;
using System.Windows;

namespace GraDeMarCoWPF.Models
{
    public class ImageFiltering : IImageFiltering
    {
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private ImageArea imageArea;
        private ImageFilterOptions filterOptions;

        private bool isActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                //FilterOriginalImage();
                System.Console.WriteLine(value.ToString());
            }
        }

        public ImageFiltering(
            ImageData imageData,
            ImageDisplay imageDisplay,
            ImageArea imageArea,
            ImageFilterOptions filterOptions)
        {
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageArea = imageArea;
            this.filterOptions = filterOptions;
        }

        public void StartFunction()
        {
            isActive = true;
        }

        public void StopFunction()
        {
            isActive = false;
        }

        public void FilterOriginalImage()
        {
            if (!isActive)
            {
                imageData.FilteredImage = imageData.OriginalImage.Clone();
                return;
            }

            int width = imageData.OriginalImage.PixelWidth;
            int height = imageData.OriginalImage.PixelHeight;
            int lowerX = imageArea.LowerX, upperX = imageArea.UpperX;
            int lowerY = imageArea.LowerY, upperY = imageArea.UpperY;
            int stride = imageData.OriginalImage.BackBufferStride;

            ImagePixels originalPixels, sourcePixels, destinationPixels1, destinationPixels2;
            {

                byte[] tmp = new byte[height * stride];
                imageData.OriginalImage.CopyPixels(tmp, stride, 0);
                originalPixels = ImagePixels.ConvertFromOneDimArray(tmp, width, height, stride);
                sourcePixels = ImagePixels.ConvertFromOneDimArray(tmp, width, height, stride);
                destinationPixels1 = ImagePixels.ConvertFromOneDimArray(tmp, width, height, stride);
                destinationPixels2 = ImagePixels.ConvertFromOneDimArray(tmp, width, height, stride);
            }

            {
                var logic = new ImageFilteringLogic();
                logic.Source = sourcePixels;
                logic.Destination = destinationPixels1;
                logic.ImageArea = imageArea;

                switch (filterOptions.BlurOption)
                {
                    case BlurOption.Gaussian:
                        logic.ImageFilter = ImageFilters.Gaussian;
                        logic.ApplyFilter();
                        logic.SwapSourceAndDestination();
                        break;
                    case BlurOption.Gaussian3Times:
                        logic.ImageFilter = ImageFilters.Gaussian;
                        for (int i = 0; i < 3; ++i)
                        {
                            logic.ApplyFilter();
                            logic.SwapSourceAndDestination();
                        }
                        break;
                    case BlurOption.Gaussian6Times:
                        logic.ImageFilter = ImageFilters.Gaussian;
                        for (int i = 0; i < 6; ++i)
                        {
                            logic.ApplyFilter();
                            logic.SwapSourceAndDestination();
                        }
                        break;
                }

                sourcePixels = logic.Source;
                destinationPixels1 = logic.Destination;
            }

            switch (filterOptions.EdgeDetectOption)
            {
                case EdgeDetectOption.Sobel:
                    var logic1 = new ImageFilteringLogic();
                    logic1.Source = sourcePixels;
                    logic1.Destination = destinationPixels1;
                    logic1.ImageArea = imageArea;
                    logic1.ImageFilter = ImageFilters.Sobel_horizontal;
                    logic1.ApplyFilter();

                    var logic2 = new ImageFilteringLogic();
                    logic2.Source = sourcePixels;
                    logic2.Destination = destinationPixels2;
                    logic2.ImageArea = imageArea;
                    logic2.ImageFilter = ImageFilters.Sobel_vertical;
                    logic2.ApplyFilter();

                    ImagePixels.ComposeTwoDirection(logic1.Destination, logic2.Destination);
                    sourcePixels = logic1.Destination;
                    break;
                case EdgeDetectOption.Laplacian:
                    var logic = new ImageFilteringLogic();
                    logic.Source = sourcePixels;
                    logic.Destination = destinationPixels1;
                    logic.ImageArea = imageArea;
                    logic.ImageFilter = ImageFilters.Laplacian;
                    logic.ApplyFilter();
                    sourcePixels = logic.Destination;
                    break;
            }

            sourcePixels.CopyRange(originalPixels, lowerX, upperX, lowerY, upperY);

            {
                byte[] tmp = ImagePixels.ConvertToOneDimArray(originalPixels, width, height, stride);
                imageData.FilteredImage.Lock();
                imageData.FilteredImage.WritePixels(new Int32Rect(0, 0, width, height), tmp, stride, 0);
                imageData.FilteredImage.Unlock();
            }
        }

        private void swap<T>(ref T target1, ref T target2)
        {
            T tmp = target1;
            target1 = target2;
            target2 = tmp;
        }

        private bool _isActive;
    }
}
