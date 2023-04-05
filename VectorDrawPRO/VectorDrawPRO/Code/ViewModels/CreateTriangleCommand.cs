using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Shape = VectorDrawPRO.Code.Models.Shape;

namespace VectorDrawPRO.Code.ViewModels;

public class CreateTriangleCommand : ICommand
{
    private readonly Canvas _canvas;

    public CreateTriangleCommand() { }

    public CreateTriangleCommand(Canvas canvas)
    {
        _canvas = canvas;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        if (parameter is Canvas canvas)
        {
            Point mousePosition = Mouse.GetPosition(canvas);

            Triangle triangle = new Triangle()
            {
                X = Convert.ToInt32(mousePosition.X),
                Y = Convert.ToInt32(mousePosition.Y),
                Width = 100,
                Height = 100
            };

            triangle.Draw(canvas);
        }
    }

    public event EventHandler CanExecuteChanged;
}

public class Triangle : Shape
{
    public override void Draw(Canvas canvas)
    {
        Polygon polygon = new Polygon();
        polygon.Points.Add(new Point(X, Y + Height));
        polygon.Points.Add(new Point(X + Width / 2, Y));
        polygon.Points.Add(new Point(X + Width, Y + Height));
        polygon.Stroke = Brushes.Black;
        canvas.Children.Add(polygon);
    }
}