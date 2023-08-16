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

        }

        public void DrawOnStaticRendering(DrawingContext drawingContext)
        {

        }

        private Point _mouseLocation;
    }
}
