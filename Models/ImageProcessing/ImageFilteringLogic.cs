using System;
using System.Threading.Tasks;

namespace GraDeMarCoWPF.Models.ImageProcessing
{
    // 3*3固定
    public class ImageFilter
    {
        public const int Size = 3;

        public int[,] Matrix { get; private set; }

        public int Denominator { get; private set; }

        public ImageFilter(int[,] matrix, int denominator)
        {
            Matrix = matrix;
            Denominator = denominator;
        }
    }

    public static class ImageFilters
    {
        public static readonly ImageFilter Gaussian = new ImageFilter(
                new int[,]
                {
                    { 1, 2, 1 },
                    { 2, 4, 2 },
                    { 1, 2, 1 }
                },
                16
            );

        public static readonly ImageFilter Sobel_horizontal = new ImageFilter(
                new int[,]
                {
                    { -1, 0, 1 },
                    { -2, 0, 2 },
                    { -1, 0, 1 }
                },
                4
            );

        public static readonly ImageFilter Sobel_vertical = new ImageFilter(
                new int[,]
                {
                    { 1, 2, 1 },
                    { 0, 0, 0 },
                    { -1, -2, -1 }
                },
                4
            );

        public static readonly ImageFilter Laplacian = new ImageFilter(
                new int[,]
                {
                    { -1, -1, -1 },
                    { -1, 8, -1 },
                    { -1, -1, -1 }
                },
                8
            );
    }

    public class ImageFilteringLogic
    {
        public ImagePixels Source { get; set; }
        
        public ImagePixels Destination { get; set; }

        public ImageArea ImageArea { get; set; }

        public ImageFilter ImageFilter { get; set; }

        public void ApplyFilter()
        {
            int lowerX = ImageArea.LowerX, upperX = ImageArea.UpperX;
            int lowerY = ImageArea.LowerY, upperY = ImageArea.UpperY;

            Source.ReflectToFrame(lowerX, upperX, lowerY, upperY);
            Destination.Clear();

            Parallel.For(lowerY, upperY + 1, applyFilterToOneLine);
        }

        private void applyFilterToOneLine(int y)
        {
            int lowerX = ImageArea.LowerX, upperX = ImageArea.UpperX;

            for (int x = lowerX; x <= upperX; ++x)
            {
                for (int c = 0; c < Constants.ColorCount; ++c)
                {
                    //int value = Destination[y, x * Constants.ColorCount + c];
                    //for (int fy = -1; fy <= 1; ++fy)
                    //{
                    //    for (int fx = -1; fx <= 1; ++fx)
                    //    {
                    //        value += Source[y + fy, (x + fx) * Constants.ColorCount + c] * ImageFilter.Matrix[fy + 1, fx + 1];
                    //    }
                    //}
                    //value = Math.Abs(value) / ImageFilter.Denominator;
                    //Destination[y, x * Constants.ColorCount + c] = value <= byte.MaxValue ? (byte)value : (byte)255;
                    int value = Destination[y, x, c];
                    for (int fy = -1; fy <= 1; ++fy)
                    {
                        for (int fx = -1; fx <= 1; ++fx)
                        {
                            value += Source[y + fy, x + fx, c] * ImageFilter.Matrix[fy + 1, fx + 1];
                        }
                    }
                    value = Math.Abs(value) / ImageFilter.Denominator;
                    Destination[y, x, c] = value <= byte.MaxValue ? (byte)value : (byte)255;
                }
            }
        }

        public void SwapSourceAndDestination()
        {
            var tmp = Source;
            Source = Destination;
            Destination = tmp;
        }
    }
}
