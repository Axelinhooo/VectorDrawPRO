using System;
using System.Windows;
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

    public static MenuItem EditShapeMenuItem;
    
    public static Shape SelectedShape;
    
    private bool individualEditmode;
    
    public static bool modified = false;

    public abstract void Draw(Canvas canvas);

    public bool isInEditMode()
    {
        return individualEditmode;
    }
    
    public void setStrokeColor(Brush color, Shape shape)
    {
        shape.Stroke = color;
    }

    public void AddMouseLeftButtonDownEvent(Shape shape, Canvas canvas)
    {
        canvas.MouseLeftButtonDown += (sender, e) =>
        {
            if (!EraserMode)
            {
                if (IsMouseOver(e.GetPosition(canvas)))
                {
                    if (individualEditmode == false && EditMode == false)
                    {
                        shape.Fill = Brushes.Red;
                        individualEditmode = true;
                        EditMode = true;
                        EditShapeMenuItem.Visibility = Visibility.Visible;
                        SelectedShape = shape;
                    }
                }
                else
                {
                    if (individualEditmode)
                    {
                        shape.Fill = modified? shape.Fill : Brushes.Transparent;
                        individualEditmode = false;
                        EditMode = false;
                        EditShapeMenuItem.Visibility = Visibility.Collapsed;
                        modified = false;
                    }
                }
            }
            else
            {
                if (IsMouseOver(e.GetPosition(canvas)))
                {
                    canvas.Children.Remove(shape);
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
