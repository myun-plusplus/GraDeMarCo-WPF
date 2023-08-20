using GraDeMarCoWPF.Models.ImageProcessing;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace GraDeMarCoWPF.Models
{
    public class PlanimetricCircleDrawing : IPlanimetricCircleDrawing
    {
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private ImageArea imageArea;
        private PlanimetricCircle planimetricCircle;
        private OutlineDrawingTool drawingTool;

        public Rect Area
        {
            get
            {
                return new Rect(firstLocation, secondLocation);
            }
        }

        private enum _State
        {
            NotActive,
            NoneSelected,
            FirstLocationSelected,
            AreaSelected
        }
        private _State state;

        private Point firstLocation
        {
            get { return _firstLocation; }
            set
            {
                _firstLocation = value;
                var rect = new Rect(
                    imageDisplay.GetAbsoluteLocation(firstLocation),
                    imageDisplay.GetAbsoluteLocation(secondLocation));
                planimetricCircle.LowerX = (int)rect.TopLeft.X;
                planimetricCircle.LowerY = (int)rect.TopLeft.Y;
                planimetricCircle.Diameter = (int)Math.Min(rect.Width, rect.Height);
            }
        }

        private Point secondLocation
        {
            get { return _secondLocation; }
            set
            {
                _secondLocation = value;
                var rect = new Rect(
                    imageDisplay.GetAbsoluteLocation(firstLocation),
                    imageDisplay.GetAbsoluteLocation(secondLocation));
                planimetricCircle.LowerX = (int)rect.TopLeft.X;
                planimetricCircle.LowerY = (int)rect.TopLeft.Y;
                planimetricCircle.Diameter = (int)Math.Min(rect.Width, rect.Height);
            }
        }

        private Point _firstLocation;
        private Point _secondLocation;

        public PlanimetricCircleDrawing(
            ImageData imageData,
            ImageDisplay imageDisplay,
            ImageArea imageArea,
            PlanimetricCircle planimetricCircle,
            OutlineDrawingTool drawingTool)
        {
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageArea = imageArea;
            this.planimetricCircle = planimetricCircle;
            this.drawingTool = drawingTool;
        }

        public void StartFunction()
        {
            state = _State.NoneSelected;
        }

        public void StopFunction()
        {
            state = _State.NotActive;
            drawCircleOnImage();
        }

        public void Click(Point location)
        {
            if (state == _State.NotActive)
            {
                return;
            }
            else if (state == _State.NoneSelected)
            {
                state = _State.FirstLocationSelected;
                firstLocation = imageDisplay.GetUnzoomedLocation(location);
                secondLocation = firstLocation;
            }
            else if (state == _State.FirstLocationSelected)
            {
                state = _State.AreaSelected;
                secondLocation = imageDisplay.GetUnzoomedLocation(location);
            }
            else
            {
                state = _State.NoneSelected;
            }
        }

        public void MouseMove(Point location)
        {
            if (state == _State.FirstLocationSelected)
            {
                secondLocation = imageDisplay.GetUnzoomedLocation(location);
            }
        }

        public void DrawOnDynamicRendering(DrawingContext drawingContext)
        {
            if (state == _State.FirstLocationSelected || state == _State.AreaSelected)
            {
                double diameter = Math.Min(secondLocation.X - firstLocation.X, secondLocation.Y - firstLocation.Y) + 1;
                Point center = new Point(
                    firstLocation.X + diameter / 2.0,
                    firstLocation.Y + diameter / 2.0);
                double radius = Math.Min(Area.Width, Area.Height) / 2.0;
                drawingContext.DrawEllipse(null, drawingTool.Pen, center, radius, radius);
            }
        }

        public void DrawOnStaticRendering(DrawingContext drawingContext)
        {
            Point center = imageDisplay.GetRelativeLocation(new Point(
                planimetricCircle.LowerX + planimetricCircle.Diameter / 2.0 + 0.5,
                planimetricCircle.LowerY + planimetricCircle.Diameter / 2.0 + 0.5));
            double coefficient = imageDisplay.DisplayedImage.Width / imageDisplay.DisplayedImage.PixelWidth;
            double radius = (planimetricCircle.Diameter - 1) * coefficient / 2.0;
            drawingContext.DrawEllipse(null, drawingTool.Pen, center, radius, radius);
        }

        public void DrawOnStaticRendering(System.Drawing.Graphics graphics)
        {
            Point relativeLowerPoint = imageDisplay.GetRelativeLocation(new Point(
                planimetricCircle.LowerX + planimetricCircle.Diameter / 2.0 + 0.5,
                planimetricCircle.LowerY + planimetricCircle.Diameter / 2.0 + 0.5));
            double coefficient = imageDisplay.DisplayedImage.Width / imageDisplay.DisplayedImage.PixelWidth;
            double radius = (planimetricCircle.Diameter - 1) * coefficient / 2.0;

            var drawingColor = System.Drawing.Color.FromArgb(
                drawingTool.Color.A,
                drawingTool.Color.R,
                drawingTool.Color.G,
                drawingTool.Color.B);

            using (var pen = new System.Drawing.Pen(drawingColor, (float)drawingTool.Pen.Thickness))
            {
                graphics.DrawEllipse(
                    pen,
                    (int)relativeLowerPoint.X,
                    (int)relativeLowerPoint.Y,
                    (int)radius,
                    (int)radius);
            }
        }

        public void DrawMaxCircle()
        {
            double centerX = (imageArea.LowerX + imageArea.UpperX) / 2.0;
            double centerY = (imageArea.LowerY + imageArea.UpperY) / 2.0;
            int diameter = Math.Min(imageArea.UpperX - imageArea.LowerX, imageArea.UpperY - imageArea.LowerY) + 1;
            planimetricCircle.LowerX = (int)(centerX - diameter / 2.0);
            planimetricCircle.LowerY = (int)(centerY - diameter / 2.0);
            planimetricCircle.Diameter = (int)diameter;

            drawCircleOnImage();
        }

        private void drawCircleOnImage()
        {
            //int width = imageData.OriginalImage.PixelWidth;
            //int height = imageData.OriginalImage.PixelHeight;
            //int stride = imageData.OriginalImage.BackBufferStride;

            //var drawingVisual = new DrawingVisual();
            //using (var drawingContext = drawingVisual.RenderOpen())
            //{
            //    drawingContext.DrawImage(imageData.OriginalImage, new Rect(0, 0, width, height));
            //    DrawOnStaticRendering(drawingContext);
            //}

            //var renderTargetBitmap = new RenderTargetBitmap(width, height, 72, 72, PixelFormats.Pbgra32);

            //RenderOptions.SetEdgeMode(renderTargetBitmap, EdgeMode.Unspecified);

            //renderTargetBitmap.Clear();
            //renderTargetBitmap.Render(drawingVisual);

            //imageData.CircledImage = new WriteableBitmap(new FormatConvertedBitmap(renderTargetBitmap, ImageData.PixelFormat, null, 0)); ;

            //{
            //    byte[] tmp = new byte[height * stride];
            //    renderTargetBitmap.CopyPixels(tmp, stride, 0);
            //    imageData.CircledImage.Lock();
            //    imageData.CircledImage.WritePixels(new Int32Rect(0, 0, width, height), tmp, stride, 0);
            //    imageData.CircledImage.Unlock();
            //}

            imageData.CircledImage = imageData.OriginalImage.Clone();
            drawCircleBySystemDrawing(imageData.CircledImage);
        }

        public void drawCircleBySystemDrawing(WriteableBitmap image)
        {
            int width = image.PixelWidth;
            int height = image.PixelHeight;
            int stride = image.BackBufferStride;

            byte[] pixels = new byte[height * stride];
            
            image.CopyPixels(pixels, stride, 0);

            {
                var canvas = new System.Drawing.Bitmap(width, height);

                var bitmapData = canvas.LockBits(
                    new System.Drawing.Rectangle(0, 0, width, height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                canvas.UnlockBits(bitmapData);

                var color = System.Drawing.Color.FromArgb(
                    drawingTool.Color.A,
                    drawingTool.Color.R,
                    drawingTool.Color.G,
                    drawingTool.Color.B);

                using (var graphics = System.Drawing.Graphics.FromImage(canvas))
                using (var pen = new System.Drawing.Pen(color, 1))
                {
                    graphics.DrawEllipse(
                        pen,
                        planimetricCircle.LowerX,
                        planimetricCircle.LowerY,
                        planimetricCircle.Diameter - 1,
                        planimetricCircle.Diameter - 1);
                }

                bitmapData = canvas.LockBits(
                    new System.Drawing.Rectangle(0, 0, width, height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Marshal.Copy(bitmapData.Scan0, pixels, 0, pixels.Length);
                canvas.UnlockBits(bitmapData);
            }

            image.Lock();
            image.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            image.Unlock();
        }
    }
}
