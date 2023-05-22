using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public class Circle : Shapes
    {
        bool individualEditmode = false;
        Lazy<Ellipse> ellipse;
        
        public int Radius { get; set; }

        public override void Draw(Canvas canvas)
        {
            ellipse = new Lazy<Ellipse>(() => new Ellipse
            {
                Width = 2 * Radius,
                Height = 2 * Radius,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            });

            Canvas.SetLeft(ellipse.Value, X - Radius);
            Canvas.SetTop(ellipse.Value, Y - Radius);

            canvas.Children.Add(ellipse.Value);

            AddMouseLeftButtonDownEvent(ellipse.Value, canvas);
        }
        
        public Shape GetShape()
        {
            return ellipse.Value;
        }
    }
}