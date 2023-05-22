using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public class Diamond : Shapes // Diamond est une classe fille de Shapes
    {
        private readonly Lazy<Polygon> polygon; // Lazy permet de ne pas instancier l'objet tant qu'il n'est pas utilisé

        public Diamond(int x, int y, int width, int height) // Constructeur de la classe Diamond
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            polygon = new Lazy<Polygon>(() => // Lazy permet de ne pas instancier l'objet tant qu'il n'est pas utilisé
            {
                var shape = new Polygon();
                shape.Points.Add(new Point(X + Width / 2, Y)); // Ajout d'un point à la liste des points du polygone
                shape.Points.Add(new Point(X + Width, Y + Height / 2));
                shape.Points.Add(new Point(X + Width / 2, Y + Height));
                shape.Points.Add(new Point(X, Y + Height / 2));
                shape.Stroke = Brushes.Black;
                shape.StrokeThickness = 1;
                return shape;
            });
        }

        public override void Draw(Canvas canvas) // Méthode Draw de la classe Diamond
        {
            canvas.Children.Add(polygon.Value); 

            AddMouseLeftButtonDownEvent(polygon.Value, canvas); 
            addMemento(polygon.Value);
        }
        
    }
}