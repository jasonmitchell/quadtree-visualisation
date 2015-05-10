using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SpatialMapping
{
    public static class VisualizationHelper
    {
        public static void DrawRectangle(Random random, Canvas canvas, Point location, float width, float height)
        {
            Rectangle rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
            rect.StrokeThickness = 1;

            rect.Width = width;
            rect.Height = height;
            Canvas.SetTop(rect, location.Y);
            Canvas.SetLeft(rect, location.X);
            canvas.Children.Add(rect);
        }
    }

    public class TestObject : IHasRectangle
    {
        private readonly Ellipse circle;

        public TestObject(Random r, Canvas quadCanvas)
        {
            circle = new Ellipse();
            circle.Fill = new SolidColorBrush(Color.FromArgb(100, (byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255)));
            circle.Width = r.Next(5, 20);
            circle.Height = circle.Width;

            Point position = new Point((r.NextDouble() * (quadCanvas.Width - (circle.Width * 2)) + circle.Width), (r.NextDouble() * (quadCanvas.Height - (circle.Width * 2)) + circle.Width));
            Rectangle = new FRectangle((float)position.X, (float)position.Y, (float)circle.Width, (float)circle.Height);
            Canvas.SetTop(circle, position.Y);
            Canvas.SetLeft(circle, position.X);
            quadCanvas.Children.Add(circle);
        }

        public FRectangle Rectangle { get; private set; }
    }
}
