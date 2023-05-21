using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models;

public abstract class Shapes
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public static bool EditMode { get; set; } = false;
    
    public static bool EraserMode = false;
    
    private bool individualEditmode;

    public abstract void Draw(Canvas canvas);
    
    public void AddMouseLeftButtonDownEvent(Shape shapes, Canvas canvas)
    {
        canvas.MouseLeftButtonDown += (sender, e) =>
        {
            if (!EraserMode)
            {
                if (IsMouseOver(e.GetPosition(canvas)))
                {
                    if (individualEditmode == false && EditMode == false)
                    {
                        shapes.Fill = Brushes.Red;
                        individualEditmode = true;
                        EditMode = true;
                    }
                }
                else
                {
                    if (individualEditmode)
                    {
                        shapes.Fill = Brushes.Transparent;
                        individualEditmode = false;
                        EditMode = false;
                    }
                }
            }
            else
            {
                if (IsMouseOver(e.GetPosition(canvas)))
                {
                    canvas.Children.Remove(shapes);
                }
            }
        };
    }
    
    public bool IsMouseOver(System.Windows.Point mousePosition)
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
