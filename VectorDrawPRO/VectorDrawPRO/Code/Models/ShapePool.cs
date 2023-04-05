using System.Collections.Generic;

namespace VectorDrawPRO.Code.Models;

public class ShapePool
{
    private readonly Stack<Shape> shapePool = new Stack<Shape>();

    public Shape GetShape()
    {
        if (shapePool.Count > 0)
        {
            return shapePool.Pop();
        }
        else
        {
            return new Circle();
        }
    }

    public void ReturnShape(Shape shape)
    {
        shapePool.Push(shape);
    }
}