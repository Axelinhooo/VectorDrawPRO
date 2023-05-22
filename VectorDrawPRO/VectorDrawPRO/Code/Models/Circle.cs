using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public class Circle : Shapes
    {
        private readonly Lazy<Ellipse> ellipse;

        public Circle(int x, int y, int width, int height,  int radius)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Radius = radius;

            ellipse = new Lazy<Ellipse>(() =>
            {
                var shape = new Ellipse();
                shape.Width = 2 * Radius;
                shape.Height = 2 * Radius;
                shape.Stroke = Brushes.Black;
                shape.StrokeThickness = 1;
                return shape;
            });
        }

        public int Radius { get; set; }

        public override void Draw(Canvas canvas)
        {
            Canvas.SetLeft(ellipse.Value, X - Radius);
            Canvas.SetTop(ellipse.Value, Y - Radius);

            canvas.Children.Add(ellipse.Value);

            AddMouseLeftButtonDownEvent(ellipse.Value, canvas);
            addMemento(ellipse.Value);
        }
    }
}