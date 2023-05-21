using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models;

public class Diamond : Shape
{
    bool individualEditmode = false;

    public override void Draw(Canvas canvas)
    {
        Polygon polygon = new Polygon();
        polygon.Points.Add(new Point(X + Width / 2, Y));
        polygon.Points.Add(new Point(X + Width, Y + Height / 2));
        polygon.Points.Add(new Point(X + Width / 2, Y + Height));
        polygon.Points.Add(new Point(X, Y + Height / 2));
        polygon.Stroke = Brushes.Black;
        canvas.Children.Add(polygon);
        
        canvas.MouseLeftButtonDown += (sender, e) =>
         {
            if (IsMouseOver(e.GetPosition(canvas)))
            {
                if(individualEditmode == false && EditMode == false)
                {
                    polygon.Fill = Brushes.Red;
                    individualEditmode = true;
                    EditMode = true;
                }
            }
            else
            {
                if (individualEditmode)
                {
                    polygon.Fill = Brushes.Transparent;
                    individualEditmode = false;
                    EditMode = false;
                }
            }
         };
    }
    
    // quand il y a un clic sur la forme, on change la couleur de la forme en rouge et son intérieur aussi
    
    
    
    
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