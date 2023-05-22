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
    Lazy<Polygon> polygon;
        
    public override void Draw(Canvas canvas)
    {
        polygon = new Lazy<Polygon>(() =>
        {
            Polygon p = new Polygon();
            p.Points.Add(new Point(X + Width / 2, Y));
            p.Points.Add(new Point(X + Width, Y + Height / 2));
            p.Points.Add(new Point(X + Width / 2, Y + Height));
            p.Points.Add(new Point(X, Y + Height / 2));
            p.Stroke = Brushes.Black;
            AddMouseLeftButtonDownEvent(p, canvas);
            return p;
        });

        canvas.Children.Add(polygon.Value);
        
        undoStack.Add(new ShapeMemento(polygon.Value, polygon.Value.Fill, polygon.Value.Stroke, polygon.Value.StrokeThickness));
    }
    
    public Shape GetShape()
    {
        return polygon.Value;
    }
}