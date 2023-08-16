using System;
using System.Linq;
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
            location = imageDisplay.GetAbsoluteLocation(location);

            Dot dot = new Dot
            {
                Location = location,
                Color = drawnDotDrawingTool.Color,
                Size = drawnDotDrawingTool.Size
            };

            drawnDotData.Dots.Add(dot);

            drawnDotData.DoneList.Add(Tuple.Create(dot.Clone(), false));
            drawnDotData.UndoList.Clear();
        }

        public void RightClick(Point location)
        {
            location = imageDisplay.GetAbsoluteLocation(location);

            var di_min = drawnDotData.Dots
                .Select(dot => getDistance(dot.Location, location))
                .Zip(Enumerable.Range(0, drawnDotData.Dots.Count), (distance, index) => Tuple.Create(distance, index))
                .Min();

            // 適当
            if (di_min.Item1 < 16.0)
            {
                Dot dot = drawnDotData.Dots[di_min.Item2];

                drawnDotData.Dots.RemoveAt(di_min.Item2);

                drawnDotData.DoneList.Add(Tuple.Create(dot, true));
                drawnDotData.UndoList.Clear();
            }
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
            if (drawnDotData.DoneList.Count == 0)
            {
                return;
            }

            var t = drawnDotData.DoneList.Last();
            if (!t.Item2)
            {
                drawnDotData.Dots.RemoveAt(drawnDotData.Dots.Count - 1);
            }
            else
            {
                drawnDotData.Dots.Add(t.Item1.Clone());
            }

            drawnDotData.UndoList.Add(t);
            drawnDotData.DoneList.RemoveAt(drawnDotData.DoneList.Count - 1);
        }

        public void Redo()
        {
            if (drawnDotData.UndoList.Count == 0)
            {
                return;
            }

            var t = drawnDotData.UndoList.Last();
            if (!t.Item2)
            {
                drawnDotData.Dots.Add(t.Item1.Clone());
            }
            else
            {
                drawnDotData.Dots.RemoveAt(drawnDotData.Dots.Count - 1);
            }

            drawnDotData.DoneList.Add(t);
            drawnDotData.UndoList.RemoveAt(drawnDotData.UndoList.Count - 1);
        }

        public void Clear()
        {
            drawnDotData.Dots.Reverse();
            drawnDotData.DoneList = drawnDotData.Dots
                .Select(dot => Tuple.Create(dot, true))
                .ToList();
            drawnDotData.UndoList.Clear();

            drawnDotData.Dots.Clear();
            detectedDotData.Dots.Clear();
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

        private static double getDistance(Point p1, Point p2)
        {
            return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }

        private Point _mouseLocation;
    }
}
