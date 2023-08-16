using System.Windows;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public class DotDrawing : IDotDrawing
    {
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private DotDrawingTool drawnDotDrawingTool;
        private DotData detectedDotData;
        private DotData drawnDotData;

        private bool isActive;

        private Point mouseLocation
        {
            get { return _mouseLocation; }
            set { _mouseLocation = value; }
        }

        public DotDrawing(
            ImageData imageData,
            ImageDisplay imageDisplay,
            DotDrawingTool drawnDotDrawingTool,
            DotData detectedDotData,
            DotData drawnDotData)
        {
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.drawnDotDrawingTool = drawnDotDrawingTool;
            this.detectedDotData = detectedDotData;
            this.drawnDotData = drawnDotData;
        }

        public void StartFunction()
        {
            isActive = true;
        }

        public void StopFunction()
        {
            isActive = false;
        }

        public void LeftClick(Point location)
        {

        }

        public void RightClick(Point location)
        {

        }

        public void MouseMove(Point location)
        {
            if (isActive)
            {
                mouseLocation = location;
            }
        }

        public void Undo()
        {

        }

        public void Redo()
        {

        }

        public void Clear()
        {

        }

        public void DrawOnDynamicRendering(DrawingContext drawingContext)
        {
            foreach (Dot dot in detectedDotData.Dots)
            {
                Point location = imageDisplay.GetRelativeLocation(dot.Location);
                location.X -= dot.Size / 2.0;
                location.Y -= dot.Size / 2.0;
                var brush = new SolidColorBrush(dot.Color);
                Rect rect = new Rect(location.X, location.Y, dot.Size, dot.Size);
                drawingContext.DrawRectangle(brush, null, rect);
            }

            foreach (Dot dot in drawnDotData.Dots)
            {
                Point location = imageDisplay.GetRelativeLocation(dot.Location);
                location.X -= dot.Size / 2.0;
                location.Y -= dot.Size / 2.0;
                var brush = new SolidColorBrush(dot.Color);
                Rect rect = new Rect(location.X, location.Y, dot.Size, dot.Size);
                drawingContext.DrawRectangle(brush, null, rect);
            }

            {
                var location = mouseLocation;
                location.X -= drawnDotDrawingTool.Size / 2.0;
                location.Y -= drawnDotDrawingTool.Size / 2.0;
                var brush = new SolidColorBrush(drawnDotDrawingTool.Color);
                Rect rect = new Rect(location.X, location.Y, drawnDotDrawingTool.Size, drawnDotDrawingTool.Size);
                drawingContext.DrawRectangle(brush, null, rect);
            }
        }

        public void DrawOnStaticRendering(DrawingContext drawingContext)
        {
            foreach (Dot dot in detectedDotData.Dots)
            {
                Point location = imageDisplay.GetRelativeLocation(dot.Location);
                location.X -= dot.Size / 2.0;
                location.Y -= dot.Size / 2.0;
                var brush = new SolidColorBrush(dot.Color);
                Rect rect = new Rect(location.X, location.Y, dot.Size, dot.Size);
                drawingContext.DrawRectangle(brush, null, rect);
            }

            foreach (Dot dot in drawnDotData.Dots)
            {
                Point location = imageDisplay.GetRelativeLocation(dot.Location);
                location.X -= dot.Size / 2.0;
                location.Y -= dot.Size / 2.0;
                var brush = new SolidColorBrush(dot.Color);
                Rect rect = new Rect(location.X, location.Y, dot.Size, dot.Size);
                drawingContext.DrawRectangle(brush, null, rect);
            }
        }

        private Point _mouseLocation;
    }
}
