using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models;

public class Circle : Shape
{
    bool individualEditmode = false;
    public int Radius { get; set; }

    public override void Draw(Canvas canvas)
    {
        Ellipse ellipse = new Ellipse
        {
            Width = 2 * Radius,
            Height = 2 * Radius,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        Canvas.SetLeft(ellipse, X - Radius);
        Canvas.SetTop(ellipse, Y - Radius);

        canvas.Children.Add(ellipse);
        
        canvas.MouseLeftButtonDown += (sender, e) =>
        {
            if (!eraserMode)
            {
               if (IsMouseOver(e.GetPosition(canvas)))
               {
                   if(individualEditmode == false && EditMode == false)
                   {
                       ellipse.Fill = Brushes.Red;
                       individualEditmode = true;
                       EditMode = true;
                   }
               }
               else
               {
                   if (individualEditmode)
                   {
                       ellipse.Fill = Brushes.Transparent;
                       individualEditmode = false;
                       EditMode = false;
                   }
               } 
            }
            else
            {
                if (IsMouseOver(e.GetPosition(canvas)))
                {
                    canvas.Children.Remove(ellipse);
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