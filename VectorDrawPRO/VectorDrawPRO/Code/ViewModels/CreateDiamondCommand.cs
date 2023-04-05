using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Shape = VectorDrawPRO.Code.Models.Shape;

namespace VectorDrawPRO.Code.ViewModels;

public class CreateDiamondCommand : ICommand
{
    private readonly Canvas _canvas;

    public CreateDiamondCommand() { }

    public CreateDiamondCommand(Canvas canvas)
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

            Diamond rectangle = new Diamond()
            {
                X = Convert.ToInt32(mousePosition.X),
                Y = Convert.ToInt32(mousePosition.Y),
                Width = 100,
                Height = 100
            };

            rectangle.Draw(canvas);
        }
    }


    public event EventHandler CanExecuteChanged;
}

public class Diamond : Shape
{
    public override void Draw(Canvas canvas)
    {
        Polygon polygon = new Polygon();
        polygon.Points.Add(new Point(X + Width / 2, Y));
        polygon.Points.Add(new Point(X + Width, Y + Height / 2));
        polygon.Points.Add(new Point(X + Width / 2, Y + Height));
        polygon.Points.Add(new Point(X, Y + Height / 2));
        polygon.Stroke = Brushes.Black;
        canvas.Children.Add(polygon);
    }
}