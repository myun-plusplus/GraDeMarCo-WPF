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
                firstLocation = imageDisplay.GetRelaiveLocation(location);
                secondLocation = firstLocation;
            }
            else if (state == _State.FirstLocationSelected)
            {
                state = _State.AreaSelected;
                secondLocation = imageDisplay.GetRelaiveLocation(location);
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
                secondLocation = imageDisplay.GetRelaiveLocation(location);
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
            drawingContext.DrawRectangle(null, drawingTool.Pen, imageArea.Area);
        }
    }
}
