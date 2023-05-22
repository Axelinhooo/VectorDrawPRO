using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public class Rectangle : Shapes
    {
        bool individualEditmode = false;
        Lazy<System.Windows.Shapes.Rectangle> rect;
        
        public override void Draw(Canvas canvas)
        {
            rect = new Lazy<System.Windows.Shapes.Rectangle>(() => new System.Windows.Shapes.Rectangle
            {
                Width = Width,
                Height = Height,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            });

            Canvas.SetLeft(rect.Value, X);
            Canvas.SetTop(rect.Value, Y);

            canvas.Children.Add(rect.Value);
            
            AddMouseLeftButtonDownEvent(rect.Value, canvas);
            
            undoStack.Add(new ShapeMemento(rect.Value, rect.Value.Fill, rect.Value.Stroke, rect.Value.StrokeThickness));
        }
        
        public Shape GetShape()
        {
            return rect.Value;
        }
    }
}