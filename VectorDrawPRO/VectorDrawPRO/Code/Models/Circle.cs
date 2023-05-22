using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public class Circle : Shapes // Circle est une classe fille de Shapes
    {
        private readonly Lazy<Ellipse> ellipse; // Lazy permet de ne pas instancier l'objet tant qu'il n'est pas utilisé

        public Circle(int x, int y, int width, int height,  int radius) // Constructeur de la classe Circle
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Radius = radius;

            ellipse = new Lazy<Ellipse>(() => // Lazy permet de ne pas instancier l'objet tant qu'il n'est pas utilisé
            {
                var shape = new Ellipse();
                shape.Width = 2 * Radius;
                shape.Height = 2 * Radius;
                shape.Stroke = Brushes.Black;
                shape.StrokeThickness = 1;
                return shape;
            });
        }

        public int Radius { get; set; }

        public override void Draw(Canvas canvas) 
        {
            Canvas.SetLeft(ellipse.Value, X - Radius); // Canvas.SetLeft permet de définir la position de l'ellipse sur l'axe X
            Canvas.SetTop(ellipse.Value, Y - Radius); // Canvas.SetTop permet de définir la position de l'ellipse sur l'axe Y

            canvas.Children.Add(ellipse.Value); // Ajout de l'ellipse à la liste des enfants du canvas

            AddMouseLeftButtonDownEvent(ellipse.Value, canvas); // Ajout de l'évènement MouseLeftButtonDown à l'ellipse
            addMemento(ellipse.Value); // Ajout de l'ellipse à la liste des mementos
        }
    }
}