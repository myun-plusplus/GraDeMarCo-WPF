using System;
using System.Windows;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    [Flags]
    public enum ImageProcessingFlags
    {
        None = 0,
        ImageArea = 1 << 0,
        PlanimetricCircle = 1 << 1,
        ImageModifying = 1 << 2,
        DrawnDots = 1 << 3
    }

    public class AppStateHandler : BindableBase
    {
        private AppData appData;
        private IImageAreaSelecting imageAreaSelecting;
        private IPlanimetricCircleDrawing planimetricCircleDrawing;
        private IImageFiltering imageFiltering;
        private IImageBinarizing imageBinarizing;
        private IGrainDetecting grainDetecting;
        private IDotDrawing dotDrawing;

        public ImageProcessingFlags ImageProcessingFlags
        {
            get { return _imageProcessingFlags; }
            set
            {
                _imageProcessingFlags = value;
                NotifyPropertyChanged(GetName.Of(() => ImageProcessingFlags));
            }
        }


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
            if (appData.CurrentState == AppState.ImageAreaSelecting)
            {
                imageAreaSelecting.DrawOnDynamicRendering(drawingContext);
            }
            else if (ImageProcessingFlags.HasFlag(ImageProcessingFlags.ImageArea))
            {
                imageAreaSelecting.DrawOnStaticRendering(drawingContext);
            }

            if (appData.CurrentState == AppState.PlanimetricCircleDrawing)
            {
                planimetricCircleDrawing.DrawOnDynamicRendering(drawingContext);
            }
            else if (ImageProcessingFlags.HasFlag(ImageProcessingFlags.PlanimetricCircle))
            {
                planimetricCircleDrawing.DrawOnStaticRendering(drawingContext);
            }

            if (appData.CurrentState == AppState.GrainDetecting)
            {
                grainDetecting.DrawOnDynamicRendering(drawingContext);
            }

            if (appData.CurrentState == AppState.DotDrawing)
            {
                dotDrawing.DrawOnDynamicRendering(drawingContext);
            }
            else if (ImageProcessingFlags.HasFlag(ImageProcessingFlags.DrawnDots))
            {
                dotDrawing.DrawOnStaticRendering(drawingContext);
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

        private ImageProcessingFlags _imageProcessingFlags;
    }
}
