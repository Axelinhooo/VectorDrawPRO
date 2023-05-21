using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models;

public class Circle : Shapes
{
    bool individualEditmode = false;
    Ellipse ellipse;
    
    public int Radius { get; set; }

    public override void Draw(Canvas canvas)
    {
        ellipse = new Ellipse
        {
            Width = 2 * Radius,
            Height = 2 * Radius,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        Canvas.SetLeft(ellipse, X - Radius);
        Canvas.SetTop(ellipse, Y - Radius);

        canvas.Children.Add(ellipse);

        AddMouseLeftButtonDownEvent(ellipse, canvas);
    }
    
    public Shape GetShape()
    {
        return ellipse;
    }
}