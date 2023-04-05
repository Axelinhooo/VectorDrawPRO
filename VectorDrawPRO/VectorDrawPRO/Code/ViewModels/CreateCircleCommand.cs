using System;
using System.Windows.Controls;
using System.Windows.Input;
using VectorDrawPRO.Code.Models;

namespace VectorDrawPRO.Code.ViewModels;

public class CreateCircleCommand : ICommand
{
    private readonly Canvas _canvas;

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
        Circle circle = new Circle
        {
            X = 100,
            Y = 100,
            Radius = 50
        };

        circle.Draw(_canvas);
    }

    public event EventHandler CanExecuteChanged;
}