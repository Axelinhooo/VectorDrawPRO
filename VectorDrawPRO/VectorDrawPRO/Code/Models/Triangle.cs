using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models;

public class Triangle : Shapes
{
    bool individualEditmode = false;
    Polygon polygon;
    
    public override void Draw(Canvas canvas)
    {
        Polygon polygon = new Polygon();
        polygon.Points.Add(new Point(X, Y + Height));
        polygon.Points.Add(new Point(X + Width / 2, Y));
        polygon.Points.Add(new Point(X + Width, Y + Height));
        polygon.Stroke = Brushes.Black;
        canvas.Children.Add(polygon);
        
        AddMouseLeftButtonDownEvent(polygon, canvas);
    }
    
    public Shape GetShape()
    {
        return polygon;
    }
}