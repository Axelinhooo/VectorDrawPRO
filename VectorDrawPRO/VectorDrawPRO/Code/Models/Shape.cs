using System.Windows.Controls;

namespace VectorDrawPRO.Code.Models;

public abstract class Shape
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public abstract void Draw(Canvas canvas);
}
