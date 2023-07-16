using System;

namespace GraDeMarCoWPF.Models.ImageProcessing
{
    public static class Constants
    {
        public const int ColorCount = 4;
    }

    public class ImagePixels
    {
        private byte[,] data;

        public static ImagePixels ConvertFromOneDimArray(byte[] oneDimArray, int width, int height, int stride)
        {
            var imagePixels = new ImagePixels();
            imagePixels.data = new byte[height + 2, (width + 2) * Constants.ColorCount];

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    for (int c = 0; c < Constants.ColorCount; ++c)
                    {
                        imagePixels.data[1 + y, (1 + x) * Constants.ColorCount + c] =
                            oneDimArray[y * stride + x * Constants.ColorCount + c];
                    }
                }
            }

            return imagePixels;
        }

        public static byte[] ConvertToOneDimArray(ImagePixels imagePixels, int width, int height, int stride)
        {
            byte[] oneDimArray = new byte[height * stride];

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    for (int c = 0; c < Constants.ColorCount; ++c)
                    {
                        oneDimArray[y * stride + x * Constants.ColorCount + c] =
                            imagePixels.data[1 + y, (1 + x) * Constants.ColorCount + c];
                    }
                }
            }

            return oneDimArray;
        }

        public static void ComposeTwoDirection(ImagePixels horizontal, ImagePixels vertical)
        {
            int length0 = horizontal.data.GetLength(0);
            int length1 = vertical.data.GetLength(1);
            for (int i0 = 0 ; i0 < length0; ++i0)
            {
                for (int i1 = 0; i1 < length1; ++i1)
                {
                    double value = Math.Sqrt(
                        horizontal.data[i0, i1] * horizontal.data[i0, i1] +
                        vertical.data[i0, i1] * vertical.data[i0, i1]);
                    horizontal.data[i0, i1] = value <= 255 ? (byte)value : (byte)255;
                }
            }
        }

        public byte this[int y, int x, int color]
        {
            get { return data[1 + y, (1 + x) * Constants.ColorCount + color]; }
            set { data[1 + y, (1 + x) * Constants.ColorCount + color] = value; }
        }

        public void Clear()
        {
            Array.Clear(data, 0, data.Length);
        }

        public void Copy(ImagePixels destination)
        {
            Array.Copy(this.data, destination.data, this.data.Length);
        }

        public void CopyRange(ImagePixels destination, int lowerX, int upperX, int lowerY, int upperY)
        {
            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    for (int c = 0; c < Constants.ColorCount; ++c)
                    {
                        destination[y, x, c] = this[y, x, c];
                    }
                }
            }
        }

        public void ReflectToFrame(int lowerX, int upperX, int lowerY, int upperY)
        {
            /*
             * ▣▤▤▤▣
             * ▥▦▦▦▥
             * ▥▦▦▦▥
             * ▥▦▦▦▥
             * ▣▤▤▤▣
             */

            // ▥ここ
            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int c = 0; c < Constants.ColorCount; ++c)
                {
                    this[y, lowerX - 1, c] = this[y, lowerX, c];
                    this[y, upperX + 1, c] = this[y, upperX, c];
                }
            }
            // ▤ここ
            for (int x = lowerX; x <= upperX; ++x)
            {
                for (int c = 0; c < Constants.ColorCount; ++c)
                {
                    this[lowerY - 1, x, c] = this[lowerY, x, c];
                    this[upperY + 1, x, c] = this[upperY, x, c];
                }
            }
            // ▣ここ
            for (int c = 0; c < Constants.ColorCount; ++c)
            {
                this[lowerY - 1, lowerX - 1, c] = this[lowerY, lowerX, c];
                this[lowerY - 1, upperX + 1, c] = this[lowerY, upperX, c];
                this[upperY + 1, lowerX - 1, c] = this[upperY, lowerX, c];
                this[upperY + 1, upperX + 1, c] = this[upperY, upperX, c];
            }
        }
    }

    public static class BasicProcessing
    {

    }
}
