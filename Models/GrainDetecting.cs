using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public class GrainDetecting : IGrainDetecting
    {
        private GrainDetectingOptions grainDetectingOptions;
        private DotDrawingTool grainInCircleDotDrawingTool;
        private DotDrawingTool grainOnCircleDotDrawingTool;

        private bool isActive;

        public GrainDetecting(
            GrainDetectingOptions grainDetectingOptions,
            DotDrawingTool grainInCircleDotDrawingTool,
            DotDrawingTool grainOnCircleDotDrawingTool)
        {
            this.grainDetectingOptions = grainDetectingOptions;
            this.grainInCircleDotDrawingTool = grainInCircleDotDrawingTool;
            this.grainOnCircleDotDrawingTool = grainOnCircleDotDrawingTool;
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

        }

        public void DrawOnDynamicRendering(DrawingContext drawingContext)
        {
            
        }
    }
}
