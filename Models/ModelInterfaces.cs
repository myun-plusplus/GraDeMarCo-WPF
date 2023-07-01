using System.Windows;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public interface IToggleFunction
    {
        void StartFunction();
        void StopFunction();
    }

    public interface IDrawingOnRenderEvent
    {
        void DrawOnRender(DrawingContext drawingContext);
    }

    public interface IDrawingOnImageSource
    {
        void DrawOnImageSource(ImageSource imageSource);
    }

    public interface IImageAreaSelecting : IToggleFunction, IDrawingOnRenderEvent, IDrawingOnImageSource
    {
        void Click(Point location);
        void MouseMove(Point location);
    }
}
