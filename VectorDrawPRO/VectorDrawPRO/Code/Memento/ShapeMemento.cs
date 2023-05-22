using System;
using System.Windows.Media;
using System.Windows.Shapes;

public class ShapeMemento
{
    public Shape Shape { get; set; }
    public Brush Fill { get; }
    public Brush Stroke { get; }
    public Double StrokeThickness { get; }

    public ShapeMemento(Shape shape, Brush fill, Brush stroke, Double strokethickness)
    {
        Shape = shape;
        Fill = fill;
        Stroke = stroke;
        StrokeThickness = strokethickness;
    }
}