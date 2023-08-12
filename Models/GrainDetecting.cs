using GraDeMarCoWPF.Models.ImageProcessing;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public class GrainDetecting : IGrainDetecting
    {
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private ImageArea imageArea;
        private PlanimetricCircle planimetricCircle;
        private OutlineDrawingTool planimetricCircleDrawingTool;
        private GrainDetectingOptions grainDetectingOptions;
        private DotDrawingTool grainInCircleDotDrawingTool;
        private DotDrawingTool grainOnCircleDotDrawingTool;
        private DotData detectedDotData;

        private bool isActive;

        public GrainDetecting(
            ImageData imageData,
            ImageDisplay imageDisplay,
            ImageArea imageArea,
            PlanimetricCircle planimetricCircle,
            OutlineDrawingTool planimetricCircleDrawingTool,
            GrainDetectingOptions grainDetectingOptions,
            DotDrawingTool grainInCircleDotDrawingTool,
            DotDrawingTool grainOnCircleDotDrawingTool,
            DotData detectedDotData)
        {
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageArea = imageArea;
            this.planimetricCircle = planimetricCircle;
            this.planimetricCircleDrawingTool = planimetricCircleDrawingTool;
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

        private static readonly int[] dx = new int[] { 1, 0, -1, 0 };
        private static readonly int[] dy = new int[] { 0, 1, 0, -1 };

        public void DetectGrains()
        {
            if (!isActive)
            {
                return;
            }

            int width = imageData.OriginalImage.PixelWidth;
            int height = imageData.OriginalImage.PixelHeight;
            int lowerX = imageArea.LowerX, upperX = imageArea.UpperX;
            int lowerY = imageArea.LowerY, upperY = imageArea.UpperY;
            int stride = imageData.OriginalImage.BackBufferStride;

            ImagePixels binarizedPixels, circledPixels;
            {
                byte[] tmp = new byte[height * stride];
                imageData.BinarizedImage.CopyPixels(tmp, stride, 0);
                binarizedPixels = ImagePixels.ConvertFromOneDimArray(tmp, width, height, stride);
                imageData.CircledImage.CopyPixels(tmp, stride, 0);
                circledPixels = ImagePixels.ConvertFromOneDimArray(tmp, width, height, stride);
            }

            bool[,] inCircle = new bool[height, width];
            {
                for (int y = lowerY; y <= upperY; ++y)
                {
                    for (int x = lowerX; x <= upperX; ++x)
                    {
                        inCircle[y, x] = circledPixels[y, x, 0] == planimetricCircleDrawingTool.Color.B &&
                            circledPixels[y, x, 1] == planimetricCircleDrawingTool.Color.G &&
                            circledPixels[y, x, 2] == planimetricCircleDrawingTool.Color.R &&
                            circledPixels[y, x, 3] == planimetricCircleDrawingTool.Color.A;
                    }
                }

                if (planimetricCircle.Diameter >= 3)
                {
                    var stack = new Stack<Tuple<int, int>>();

                    inCircle[
                        planimetricCircle.LowerY + planimetricCircle.Diameter / 2,
                        planimetricCircle.LowerX + planimetricCircle.Diameter / 2] = true;
                    stack.Push(Tuple.Create(
                        planimetricCircle.LowerX + planimetricCircle.Diameter / 2,
                        planimetricCircle.LowerY + planimetricCircle.Diameter / 2));

                    while (stack.Count != 0)
                    {
                        var t = stack.Pop();

                        for (int d = 0; d < 4; ++d)
                        {
                            int nx = t.Item1 + dx[d], ny = t.Item2 + dy[d];
                            if (nx < lowerX || upperX < nx || ny < lowerY || upperY < ny || inCircle[ny, nx])
                            {
                                continue;
                            }
                            inCircle[ny, nx] = true;
                            stack.Push(Tuple.Create(nx, ny));
                        }
                    }
                }
            }

            bool[,] isWhite = new bool[height, width];
            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    isWhite[y, x] = binarizedPixels[y, x, 0] == 255;
                }
            }

            List<Point> dotLocationsInCircle = new List<Point>();
            List<Point> dotLocationsOnCircle = new List<Point>();
            {
                bool[,] isVisited = new bool[height, width];
                var stack = new Stack<Tuple<int, int>>();

                for (int y = lowerY; y <= upperY; ++y)
                {
                    for (int x = lowerX; x <= upperX; ++x)
                    {
                        if (!inCircle[y, x] || !isWhite[y, x] || isVisited[y, x])
                        {
                            continue;
                        }

                        int pixelCount = 1;
                        bool onCircle = !inCircle[y, x];
                        long sumX = x, sumY = y;
                        isVisited[y, x] = true;
                        stack.Push(Tuple.Create(x, y));

                        while (stack.Count != 0)
                        {
                            var t = stack.Pop();

                            for (int d = 0; d < 4; ++d)
                            {
                                int nx = t.Item1 + dx[d], ny = t.Item2 + dy[d];
                                if (nx < lowerX || upperX < nx || ny < lowerY || upperY < ny || isVisited[ny, nx])
                                {
                                    continue;
                                }
                                if (!isWhite[ny, nx])
                                {
                                    continue;
                                }
                                ++pixelCount;
                                if (!inCircle[ny, nx])
                                {
                                    onCircle = true;
                                }
                                sumX += nx;
                                sumY += ny;
                                isVisited[ny, nx] = true;
                                stack.Push(Tuple.Create(nx, ny));
                            }
                        }

                        if (pixelCount >= grainDetectingOptions.MinimumGrainPixels)
                        {
                            sumX /= pixelCount;
                            sumY /= pixelCount;

                            if (!onCircle)
                            {
                                if (grainDetectingOptions.DetectsGrainsInCircle)
                                {
                                    dotLocationsInCircle.Add(new Point((int)sumX, (int)sumY));
                                }
                            }
                            else
                            {
                                if (grainDetectingOptions.DetectsGrainsOnCircle)
                                {
                                    dotLocationsOnCircle.Add(new Point((int)sumX, (int)sumY));
                                }
                            }
                        }
                    }
                }
            }

            detectedDotData.Dots.Clear();

            foreach (Point location in dotLocationsInCircle)
            {
                detectedDotData.Dots.Add(new Dot()
                {
                    Location = location,
                    Color = grainInCircleDotDrawingTool.Color,
                    Size = grainInCircleDotDrawingTool.Size,
                });
            }
            foreach (Point location in dotLocationsOnCircle)
            {
                detectedDotData.Dots.Add(new Dot()
                {
                    Location = location,
                    Color = grainOnCircleDotDrawingTool.Color,
                    Size = grainOnCircleDotDrawingTool.Size,
                });
            }

            //detectedDotData.Dots.Add(new Dot() { Location = new Point(50, 50), Color = Colors.Red, Size = 5.0 });
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
        }
    }
}
