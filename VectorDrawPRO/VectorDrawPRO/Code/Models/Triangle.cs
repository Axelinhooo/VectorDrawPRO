using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    namespace VectorDrawPRO.Code.Models
    {
        public class Triangle : Shapes
        {
            bool individualEditmode = false;
            Lazy<Polygon> polygon;
        
            public override void Draw(Canvas canvas)
            {
                polygon = new Lazy<Polygon>(() =>
                {
                    Polygon p = new Polygon();
                    p.Points.Add(new Point(X, Y + Height));
                    p.Points.Add(new Point(X + Width / 2, Y));
                    p.Points.Add(new Point(X + Width, Y + Height));
                    p.Stroke = Brushes.Black;
                    AddMouseLeftButtonDownEvent(p, canvas);
                    return p;
                });

                canvas.Children.Add(polygon.Value);
            }
        
            public Shape GetShape()
            {
                return polygon.Value;
            }
        }
    }
}