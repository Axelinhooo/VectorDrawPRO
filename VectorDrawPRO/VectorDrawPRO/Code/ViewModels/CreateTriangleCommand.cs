using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VectorDrawPRO.Code.Models;

namespace VectorDrawPRO.Code.ViewModels;

public class CreateTriangleCommand : ICommand
{
    private readonly Canvas _canvas;
    public static bool _isSelected = false;

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
                X = Convert.ToInt32(mousePosition.X) - 50,
                Y = Convert.ToInt32(mousePosition.Y) - 50,
                Width = 100,
                Height = 100
            };

            triangle.Draw(canvas);
        }
    }

    public event EventHandler CanExecuteChanged;
}