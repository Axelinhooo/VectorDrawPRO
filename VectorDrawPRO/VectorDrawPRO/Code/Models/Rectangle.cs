using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VectorDrawPRO.Code.Models;

public class Rectangle : Shape
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
        
        canvas.MouseLeftButtonDown += (sender, e) =>
        {
            if (IsMouseOver(e.GetPosition(canvas)))
            {
                if(individualEditmode == false && EditMode == false)
                {
                    rect.Fill = Brushes.Red;
                    individualEditmode = true;
                    EditMode = true;
                }
            }
            else
            {
                if (individualEditmode)
                {
                    rect.Fill = Brushes.Transparent;
                    individualEditmode = false;
                    EditMode = false;
                }
            }
        };
    }
    
    public bool IsMouseOver(Point mousePosition)
    {
        if (mousePosition.X >= X && mousePosition.X <= X + Width && mousePosition.Y >= Y && mousePosition.Y <= Y + Height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}