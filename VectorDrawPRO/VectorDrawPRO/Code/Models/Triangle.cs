using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public class Triangle : Shapes
    {
        private readonly Lazy<Polygon> polygon; // Lazy permet de ne pas instancier l'objet tant qu'il n'est pas utilisé

        public Triangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            polygon = new Lazy<Polygon>(() =>
            {
                Polygon p = new Polygon();
                p.Points.Add(new Point(X, Y + Height)); // Ajout d'un point à la liste des points du polygone
                p.Points.Add(new Point(X + Width / 2, Y));
                p.Points.Add(new Point(X + Width, Y + Height));
                p.Stroke = Brushes.Black; // Couleur du contour
                return p;
            });
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Children.Add(polygon.Value);
            AddMouseLeftButtonDownEvent(polygon.Value, canvas);
            addMemento(polygon.Value);
        }
    }
}