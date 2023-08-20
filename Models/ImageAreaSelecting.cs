using System.Windows;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public class ImageAreaSelecting : IImageAreaSelecting
    {
        private ImageDisplay imageDisplay;
        private ImageArea imageArea;
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
                imageArea.LowerX = (int)rect.TopLeft.X;
                imageArea.LowerY = (int)rect.TopLeft.Y;
                imageArea.UpperX = (int)rect.BottomRight.X;
                imageArea.UpperY = (int)rect.BottomRight.Y;
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
                imageArea.LowerX = (int)rect.TopLeft.X;
                imageArea.LowerY = (int)rect.TopLeft.Y;
                imageArea.UpperX = (int)rect.BottomRight.X;
                imageArea.UpperY = (int)rect.BottomRight.Y;
            }
        }

        private Point _firstLocation;
        private Point _secondLocation;

        public ImageAreaSelecting(ImageDisplay imageDisplay, ImageArea imageArea, OutlineDrawingTool drawingTool)
        {
            this.imageDisplay = imageDisplay;
            this.imageArea = imageArea;
            this.drawingTool = drawingTool;
        }

        public void StartFunction()
        {
            state = _State.NoneSelected;
        }

        public void StopFunction()
        {
            state = _State.NotActive;
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
                drawingContext.DrawRectangle(null, drawingTool.Pen, Area);
            }
        }

        public void DrawOnStaticRendering(DrawingContext drawingContext)
        {
            Point relativeLowerPoint = imageDisplay.GetRelativeLocation(new Point(imageArea.LowerX, imageArea.LowerY));
            Point relativeUpperPoint = imageDisplay.GetRelativeLocation(new Point(imageArea.UpperX, imageArea.UpperY));

            drawingContext.DrawRectangle(null, drawingTool.Pen, new Rect(relativeLowerPoint, relativeUpperPoint));
        }

        public void DrawOnStaticRendering(System.Drawing.Graphics graphics)
        {
            Point relativeLowerPoint = imageDisplay.GetRelativeLocation(new Point(imageArea.LowerX, imageArea.LowerY));
            Point relativeUpperPoint = imageDisplay.GetRelativeLocation(new Point(imageArea.UpperX, imageArea.UpperY));

            var drawingColor = System.Drawing.Color.FromArgb(
                drawingTool.Color.A,
                drawingTool.Color.R,
                drawingTool.Color.G,
                drawingTool.Color.B);

            using (var pen = new System.Drawing.Pen(drawingColor, (float)drawingTool.Pen.Thickness))
            {
                graphics.DrawRectangle(pen, new System.Drawing.Rectangle(
                    (int)relativeLowerPoint.X,
                    (int)relativeLowerPoint.Y,
                    (int)(relativeUpperPoint.X - relativeLowerPoint.X),
                    (int)(relativeUpperPoint.Y - relativeLowerPoint.Y)));
            }
        }
    }
}
