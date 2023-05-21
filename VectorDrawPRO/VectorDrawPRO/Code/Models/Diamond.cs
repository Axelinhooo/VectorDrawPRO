using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models;

public class Diamond : Shapes
{
    bool individualEditmode = false;
    Polygon polygon;

    public override void Draw(Canvas canvas)
    {
        polygon = new Polygon();
        polygon.Points.Add(new Point(X + Width / 2, Y));
        polygon.Points.Add(new Point(X + Width, Y + Height / 2));
        polygon.Points.Add(new Point(X + Width / 2, Y + Height));
        polygon.Points.Add(new Point(X, Y + Height / 2));
        polygon.Stroke = Brushes.Black;
        canvas.Children.Add(polygon);
        
        AddMouseLeftButtonDownEvent(polygon, canvas);
    }
    
    public Shape GetShape()
    {
        return polygon;
    }
}