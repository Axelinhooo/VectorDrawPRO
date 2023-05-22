using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VectorDrawPRO.Code.Models;

namespace VectorDrawPRO.Code.ViewModels;

public class CreateCircleCommand : ICommand
{
    private readonly Canvas _canvas;
    public static bool _isSelected = false;

    public CreateCircleCommand(Canvas canvas)
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

            Circle circle = new Circle(Convert.ToInt32(mousePosition.X),Convert.ToInt32(mousePosition.Y),100 ,75 ,50)
            {
                X = Convert.ToInt32(mousePosition.X),
                Y = Convert.ToInt32(mousePosition.Y),
                Width = 100,
                Height = 75,
                Radius = 50
            };

            circle.Draw(canvas);
        }
    }

    public event EventHandler CanExecuteChanged;
}