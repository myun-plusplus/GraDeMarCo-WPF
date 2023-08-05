using GraDeMarCoWPF.Models.ImageProcessing;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            renderCircleOnImage();
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
                double diameter = Math.Min(secondLocation.X - firstLocation.X, secondLocation.Y - firstLocation.Y);
                Point center = new Point(firstLocation.X + diameter / 2.0, firstLocation.Y + diameter / 2.0);
                double radius = Math.Min(Area.Width, Area.Height) / 2.0;
                drawingContext.DrawEllipse(null, drawingTool.Pen, center, radius, radius);
            }
        }

        public void DrawOnStaticRendering(DrawingContext drawingContext)
        {
            Point center = imageDisplay.GetRelativeLocation(new Point(
                planimetricCircle.LowerX + planimetricCircle.Diameter / 2.0,
                planimetricCircle.LowerY + planimetricCircle.Diameter / 2.0));
            double coefficient = imageDisplay.DisplayedImage.Width / imageDisplay.DisplayedImage.PixelWidth;
            double radius = planimetricCircle.Diameter * coefficient / 2.0;
            drawingContext.DrawEllipse(null, drawingTool.Pen, center, radius, radius);
        }

        public void DrawMaxCircle()
        {
            double centerX = (imageArea.LowerX + imageArea.UpperX) / 2.0;
            double centerY = (imageArea.LowerY + imageArea.UpperY) / 2.0;
            int diameter = Math.Min(imageArea.UpperX - imageArea.LowerX, imageArea.UpperY - imageArea.LowerY);
            planimetricCircle.LowerX = (int)(centerX - diameter / 2.0);
            planimetricCircle.LowerY = (int)(centerY - diameter / 2.0);
            planimetricCircle.Diameter = (int)diameter;

            renderCircleOnImage();
        }

        private void renderCircleOnImage()
        {
            int width = imageData.OriginalImage.PixelWidth;
            int height = imageData.OriginalImage.PixelHeight;
            int stride = imageData.OriginalImage.BackBufferStride;

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(imageData.OriginalImage, new Rect(0, 0, width, height));
                DrawOnStaticRendering(drawingContext);
            }

            var renderTargetBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Clear();
            renderTargetBitmap.Render(drawingVisual);

            {
                byte[] tmp = new byte[height * stride];
                renderTargetBitmap.CopyPixels(tmp, stride, 0);
                imageData.CircledImage.Lock();
                imageData.CircledImage.WritePixels(new Int32Rect(0, 0, width, height), tmp, stride, 0);
                imageData.CircledImage.Lock();
            }
        }
    }
}
