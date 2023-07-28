using System.Windows;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public class GrainDetecting : IGrainDetecting
    {
        private ImageDisplay imageDisplay;
        private PlanimetricCircle planimetricCircle;
        private GrainDetectingOptions grainDetectingOptions;
        private DotDrawingTool grainInCircleDotDrawingTool;
        private DotDrawingTool grainOnCircleDotDrawingTool;
        private DotData detectedDotData;

        private bool isActive;

        public GrainDetecting(
            ImageDisplay imageDisplay,
            PlanimetricCircle planimetricCircle,
            GrainDetectingOptions grainDetectingOptions,
            DotDrawingTool grainInCircleDotDrawingTool,
            DotDrawingTool grainOnCircleDotDrawingTool,
            DotData detectedDotData)
        {
            this.imageDisplay = imageDisplay;
            this.planimetricCircle = planimetricCircle;
            this.grainDetectingOptions = grainDetectingOptions;
            this.grainInCircleDotDrawingTool = grainInCircleDotDrawingTool;
            this.grainOnCircleDotDrawingTool = grainOnCircleDotDrawingTool;
            this.detectedDotData = detectedDotData;
        }

        public void StartFunction()
        {
            isActive = true;
        }

        public void StopFunction()
        {
            isActive = false;
        }

        public void DetectGrains()
        {
            detectedDotData.Dots.Clear();

            if (!isActive)
            {
                return;
            }

            detectedDotData.Dots.Add(new Dot() { Location = new Point(50, 50), Color = Colors.Red, Size = 5.0 });
        }

        public void DrawOnDynamicRendering(DrawingContext drawingContext)
        {
            var brush = new SolidColorBrush(Colors.Transparent);

            foreach (Dot dot in detectedDotData.Dots)
            {
                Point location = imageDisplay.GetRelativeLocation(dot.Location);
                location.X -= dot.Size / 2.0;
                location.Y -= dot.Size / 2.0;
                brush.Color = dot.Color;
                Rect rect = new Rect(location.X, location.Y, dot.Size, dot.Size);
                drawingContext.DrawRectangle(brush, null, rect);
            }
        }
    }
}
