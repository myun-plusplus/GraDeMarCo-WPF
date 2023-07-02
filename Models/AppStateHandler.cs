using System.Windows;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public class AppStateHandler
    {
        private AppData appData;
        private ImageAreaSelecting imageAreaSelecting;
        private PlanimetricCircleDrawing planimetricCircleDrawing;


        public AppStateHandler(AppData appData,
            ImageAreaSelecting imageAreaSelecting,
            PlanimetricCircleDrawing planetricCircleDrawing)
        {
            this.appData = appData;
            this.imageAreaSelecting = imageAreaSelecting;
            this.planimetricCircleDrawing = planetricCircleDrawing;
        }

        public void DrawOnRender(DrawingContext drawingContext)
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
                    imageAreaSelecting.DrawOnRender(drawingContext);
                    break;
                case AppState.PlanimetricCircleDrawing:
                    planimetricCircleDrawing.DrawOnRender(drawingContext);
                    break;
            }
        }

        public void LeftClick(Point location)
        {

        }
    }
}
