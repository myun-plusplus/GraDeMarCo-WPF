using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;

namespace GraDeMarCoWPF.Models
{
    [Flags]
    public enum ImageModificationFlags
    {
        None = 0,
        ImageArea = 1 << 0,
        PlanimetricCircle = 1 << 1,
        ImageModifying = 1 << 2,
        DrawnDots = 1 << 3
    }

    public class ImageModification : BindableBase
    {
        private AppData appData;
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private IImageAreaSelecting imageAreaSelecting;
        private IPlanimetricCircleDrawing planimetricCircleDrawing;
        private IDotDrawing dotDrawing;

        public ImageModificationFlags ImageModificationlags
        {
            get { return _imageProcessingFlags; }
            set
            {
                _imageProcessingFlags = value;
                NotifyPropertyChanged(GetName.Of(() => ImageModificationlags));
            }
        }


        public ImageModification(
            AppData appData,
            ImageData imageData,
            ImageDisplay imageDisplay,
            IImageAreaSelecting imageAreaSelecting,
            IPlanimetricCircleDrawing planimetricCircleDrawing,
            IDotDrawing dotDrawing)
        {
            this.appData = appData;
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageAreaSelecting = imageAreaSelecting;
            this.planimetricCircleDrawing = planimetricCircleDrawing;
            this.dotDrawing = dotDrawing;
        }

        public void ApplyImageProcessing()
        {
            int width = imageData.OriginalImage.PixelWidth;
            int height = imageData.OriginalImage.PixelHeight;
            int stride = imageData.OriginalImage.BackBufferStride;

            byte[] pixels = new byte[height * stride];

            if (ImageModificationlags.HasFlag(ImageModificationFlags.ImageModifying))
            {
                imageData.BinarizedImage.CopyPixels(pixels, stride, 0);
            }
            else
            {
                imageData.OriginalImage.CopyPixels(pixels, stride, 0);
            }

            {
                var canvas = new Bitmap(width, height);

                var bitmapData = canvas.LockBits(
                    new Rectangle(0, 0, width, height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                canvas.UnlockBits(bitmapData);

                using (var graphics = Graphics.FromImage(canvas))
                {
                    if (ImageModificationlags.HasFlag(ImageModificationFlags.ImageArea))
                    {
                        imageAreaSelecting.DrawOnStaticRendering(graphics);
                    }

                    if (ImageModificationlags.HasFlag(ImageModificationFlags.PlanimetricCircle))
                    {
                        planimetricCircleDrawing.DrawOnStaticRendering(graphics);
                    }

                    if (ImageModificationlags.HasFlag(ImageModificationFlags.DrawnDots))
                    {
                        dotDrawing.DrawOnStaticRendering(graphics);
                    }
                }

                bitmapData = canvas.LockBits(
                    new Rectangle(0, 0, width, height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Marshal.Copy(bitmapData.Scan0, pixels, 0, pixels.Length);
                canvas.UnlockBits(bitmapData);
            }

            imageDisplay.DisplayedImage.Lock();
            imageDisplay.DisplayedImage.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            imageDisplay.DisplayedImage.Unlock();
        }

        private ImageModificationFlags _imageProcessingFlags;
    }
}
