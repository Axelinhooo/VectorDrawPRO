using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models;

public class Rectangle : Shapes
{
    bool individualEditmode = false;
    System.Windows.Shapes.Rectangle rect;
    
    public override void Draw(Canvas canvas)
    {
        rect = new System.Windows.Shapes.Rectangle
        {
            Width = Width,
            Height = Height,
            Stroke = Brushes.Black,
            StrokeThickness = 1
        };

        Canvas.SetLeft(rect, X);
        Canvas.SetTop(rect, Y);

        canvas.Children.Add(rect);
        
        AddMouseLeftButtonDownEvent(rect, canvas);
    }
    
    public Shape GetShape()
    {
        return rect;
    }
}