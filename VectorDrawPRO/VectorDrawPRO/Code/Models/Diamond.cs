using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models;

public class Diamond : Shape
{
    public override void Draw(Canvas canvas)
    {
        Polygon polygon = new Polygon();
        polygon.Points.Add(new Point(X + Width / 2, Y));
        polygon.Points.Add(new Point(X + Width, Y + Height / 2));
        polygon.Points.Add(new Point(X + Width / 2, Y + Height));
        polygon.Points.Add(new Point(X, Y + Height / 2));
        polygon.Stroke = Brushes.Black;
        canvas.Children.Add(polygon);
    }
    
    private bool IsPointInsideDiamond(Point point, double x, double y, double width, double height)
    {
        // Calculer les coordonnées du centre du losange
        double centerX = x + width / 2;
        double centerY = y + height / 2;

        // Calculer les distances entre le point et le centre du losange
        double dx = Math.Abs(point.X - centerX);
        double dy = Math.Abs(point.Y - centerY);

        // Vérifier si le point est à l'intérieur du losange
        return (dx / width) + (dy / height) <= 0.5;
    }
}