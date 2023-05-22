using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public class Triangle : Shapes
    {
        private readonly Lazy<Polygon> polygon;

        public Triangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            polygon = new Lazy<Polygon>(() =>
            {
                Polygon p = new Polygon();
                p.Points.Add(new Point(X, Y + Height));
                p.Points.Add(new Point(X + Width / 2, Y));
                p.Points.Add(new Point(X + Width, Y + Height));
                p.Stroke = Brushes.Black;
                return p;
            });
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Children.Add(polygon.Value);
            AddMouseLeftButtonDownEvent(polygon.Value, canvas);
            addMemento(polygon.Value);
        }

        public Polygon GetPolygon()
        {
            return polygon.Value;
        }
    }
}