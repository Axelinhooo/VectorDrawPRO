using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VectorDrawPRO.Code.Models;

public class Rectangle : Shapes
{
    bool individualEditmode = false;
    
    public override void Draw(Canvas canvas)
    {
        System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle
        {
            Width = Width,
            Height = Height,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        Canvas.SetLeft(rect, X);
        Canvas.SetTop(rect, Y);

        canvas.Children.Add(rect);
        
        AddMouseLeftButtonDownEvent(rect, canvas);
    }
}