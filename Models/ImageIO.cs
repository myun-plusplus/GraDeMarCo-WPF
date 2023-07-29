using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraDeMarCoWPF.Models
{
    public class ImageIO
    {
        private ImageData imageData;
        private ImageDisplay imageDisplay;

        public ImageIO(ImageData imageData, ImageDisplay imageDisplay)
        {
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
        }

        public void LoadImageFile(string filePath)
        {
            var bitmapSource = new BitmapImage(new Uri(filePath));
            imageData.OriginalImage = new WriteableBitmap(bitmapSource);
            imageData.CircledImage = new WriteableBitmap(bitmapSource);
            imageData.FilteredImage = new WriteableBitmap(bitmapSource);
            imageData.BinarizedImage = new WriteableBitmap(bitmapSource);
        }

        public void SaveDisplayedImage(string filePath)
        {
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(
                    imageDisplay.DisplayedImage,
                    new Rect(new Size(imageData.OriginalImage.PixelWidth, imageData.OriginalImage.PixelHeight)));
            }

            // dpiわからない
            var renderTargetBitmap = new RenderTargetBitmap(
                imageData.OriginalImage.PixelWidth,
                imageData.OriginalImage.PixelHeight,
                96,
                96,
                PixelFormats.Pbgra32);
            renderTargetBitmap.Render(drawingVisual);
            var bitmapFrame = BitmapFrame.Create(renderTargetBitmap);

            BitmapEncoder encoder;

            string extension = Path.GetExtension(filePath);
            switch (extension)
            {
                case ".bmp":
                    encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(bitmapFrame);
                    break;
                case ".exif":
                    throw new Exception("Image format EXIF is not supported.");
                case ".gif":
                    encoder = new GifBitmapEncoder();
                    encoder.Frames.Add(bitmapFrame);
                    break;
                case ".jpg":
                    encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(bitmapFrame);
                    break;
                case ".png":
                    encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(bitmapFrame);
                    break;
                case ".tiff":
                    encoder = new TiffBitmapEncoder();
                    encoder.Frames.Add(bitmapFrame);
                    break;
                default:
                    encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(bitmapFrame);
                    break;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }
    }
}
