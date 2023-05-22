using System.Windows.Media;
using System.Windows.Shapes;

public class ShapeMemento
{
    public Shape Shape { get; set; }
    public Brush Fill { get; }
    public Brush Stroke { get; }
    public double StrokeThickness { get; }

    public ShapeMemento(Shape shape, Brush fill, Brush stroke, double strokeThickness)
    {
        Shape = shape;
        Fill = fill;
        Stroke = stroke;
        StrokeThickness = strokeThickness;
    }

    private ShapeMemento() { }

    public static ShapeMemento CreateEmpty()
    {
        return new ShapeMemento();
    }
}