using System.Windows;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public class AppStateHandler
    {
        private AppData appData;
        private IImageAreaSelecting imageAreaSelecting;
        private IPlanimetricCircleDrawing planimetricCircleDrawing;
        private IGrainDetecting grainDetecting;
        private IDotDrawing dotDrawing;


        public AppStateHandler(AppData appData,
            IImageAreaSelecting imageAreaSelecting,
            IPlanimetricCircleDrawing planimetricCircleDrawing,
            IGrainDetecting grainDetecting,
            IDotDrawing dotDrawing)
        {
            this.appData = appData;
            this.imageAreaSelecting = imageAreaSelecting;
            this.planimetricCircleDrawing = planimetricCircleDrawing;
            this.grainDetecting = grainDetecting;
            this.dotDrawing = dotDrawing;
        }

        public void DrawOnRender(DrawingContext drawingContext)
        {
            switch (appData.CurrentState)
            {
                case AppState.ImageAreaSelecting:
                    imageAreaSelecting.DrawOnDynamicRendering(drawingContext);
                    break;
                case AppState.PlanimetricCircleDrawing:
                    planimetricCircleDrawing.DrawOnDynamicRendering(drawingContext);
                    break;
                case AppState.GrainDetecting:
                    grainDetecting.DrawOnDynamicRendering(drawingContext);
                    break;
                case AppState.DotDrawing:
                    dotDrawing.DrawOnDynamicRendering(drawingContext);
                    break;
            }
        }

        public void MouseMove(Point location)
        {
            switch (appData.CurrentState)
            {
                case AppState.None:
                    break;
                case AppState.WorkspacePrepared:
                    break;
                case AppState.ImageOpened:
                    break;
                case AppState.ImageAreaSelecting:
                    imageAreaSelecting.MouseMove(location);
                    break;
                case AppState.PlanimetricCircleDrawing:
                    planimetricCircleDrawing.MouseMove(location);
                    break;
                case AppState.DotDrawing:
                    dotDrawing.MouseMove(location);
                    break;
            }
        }

        public void LeftClick(Point location)
        {
            switch (appData.CurrentState)
            {
                case AppState.None:
                    break;
                case AppState.WorkspacePrepared:
                    break;
                case AppState.ImageOpened:
                    break;
                case AppState.ImageAreaSelecting:
                    imageAreaSelecting.Click(location);
                    break;
                case AppState.PlanimetricCircleDrawing:
                    planimetricCircleDrawing.Click(location);
                    break;
                case AppState.DotDrawing:
                    dotDrawing.LeftClick(location);
                    break;
            }
        }

        public void RightClick(Point location)
        {
            switch(appData.CurrentState)
            {
                case AppState.DotDrawing:
                    dotDrawing.RightClick(location);
                    break;
            }
        }
    }
}
