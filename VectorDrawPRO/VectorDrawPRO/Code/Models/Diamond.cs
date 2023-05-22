using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public class Diamond : Shapes
    {
        private readonly Lazy<Polygon> polygon;

        public Diamond(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            polygon = new Lazy<Polygon>(() =>
            {
                var shape = new Polygon();
                shape.Points.Add(new Point(X + Width / 2, Y));
                shape.Points.Add(new Point(X + Width, Y + Height / 2));
                shape.Points.Add(new Point(X + Width / 2, Y + Height));
                shape.Points.Add(new Point(X, Y + Height / 2));
                shape.Stroke = Brushes.Black;
                shape.StrokeThickness = 1;
                return shape;
            });
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Children.Add(polygon.Value);

            AddMouseLeftButtonDownEvent(polygon.Value, canvas);
            addMemento(polygon.Value);
        }
        
    }
}