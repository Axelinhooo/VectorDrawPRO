using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public class Rectangle : Shapes
    {
        private readonly Lazy<System.Windows.Shapes.Rectangle> rect; // Lazy permet de ne pas instancier l'objet tant qu'il n'est pas utilisé

        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            rect = new Lazy<System.Windows.Shapes.Rectangle>(() => new System.Windows.Shapes.Rectangle 
            {
                Width = Width,
                Height = Height,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            });
        }

        public override void Draw(Canvas canvas)
        {
            Canvas.SetLeft(rect.Value, X);
            Canvas.SetTop(rect.Value, Y);

            canvas.Children.Add(rect.Value);

            AddMouseLeftButtonDownEvent(rect.Value, canvas);
            addMemento(rect.Value);
        }
    }
}