using System.Windows.Controls;

namespace VectorDrawPRO.Code.Models;

public abstract class Shape
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public static bool EditMode { get; set; } = false;

    public abstract void Draw(Canvas canvas);
}
