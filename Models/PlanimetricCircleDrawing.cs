using System;
using System.Windows;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public class PlanimetricCircleDrawing : IPlanimetricCircleDrawing
    {
        private ImageDisplay imageDisplay;
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

        public PlanimetricCircleDrawing(ImageDisplay imageDisplay, PlanimetricCircle planimetricCircle, OutlineDrawingTool drawingTool)
        {
            this.imageDisplay = imageDisplay;
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

        public void DrawOnRender(DrawingContext drawingContext)
        {
            if (state == _State.FirstLocationSelected || state == _State.AreaSelected)
            {
                Point c = new Point((firstLocation.X + secondLocation.X) / 2.0, (firstLocation.Y + secondLocation.Y) / 2.0);
                double r = Math.Min(Area.Width, Area.Height) / 2.0;
                drawingContext.DrawEllipse(null, drawingTool.Pen, c, r, r);
            }
        }

        public void DrawOnImageSource(ImageSource imageSource)
        {

        }
    }
}
