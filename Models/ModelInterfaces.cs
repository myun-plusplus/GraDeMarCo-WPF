using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public interface IToggleFunction
    {
        void Start();
        void Stop();
    }

    public interface IDrawingOnRender
    {
        void DrawOnRender(DrawingContext drawingContext);
    }

    public interface IDrawingOnImageSource
    {
        void DrawOnImageSource(ImageSource imageSource);
    }
}
