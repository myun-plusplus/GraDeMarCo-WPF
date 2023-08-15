using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public interface IToggleFunction
    {
        void StartFunction();
        void StopFunction();
    }

    public interface IDrawingOnDynamicRendering
    {
        void DrawOnDynamicRendering(DrawingContext drawingContext);
    }

    public interface IDrawingOnStaticRendering
    {
        void DrawOnStaticRendering(DrawingContext drawingContext);
    }

    public interface IImageAreaSelecting : IToggleFunction, IDrawingOnDynamicRendering, IDrawingOnStaticRendering
    {
        void Click(Point location);
        void MouseMove(Point location);
    }
    public interface IPlanimetricCircleDrawing : IToggleFunction, IDrawingOnDynamicRendering, IDrawingOnStaticRendering
    {
        void Click(Point location);
        void MouseMove(Point location);
        void DrawMaxCircle();
    }

    public interface IImageFiltering : IToggleFunction
    {
        void FilterOriginalImage();
    }

    public interface IImageBinarizing : IToggleFunction
    {
        void BinarizeFilteredImage();
    }

    public interface IGrainDetecting : IToggleFunction, IDrawingOnDynamicRendering
    {
        void DetectGrains();
    }

    public interface IDotDrawing : IToggleFunction, IDrawingOnStaticRendering, IDrawingOnDynamicRendering
    {
        void Click();
        void RightClick();
        void MouseMove();
        void Undo();
        void Redo();
        void CLear();
    }
}
