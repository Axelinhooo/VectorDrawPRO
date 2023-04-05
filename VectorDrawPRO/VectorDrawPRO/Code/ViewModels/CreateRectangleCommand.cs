using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VectorDrawPRO.Code.Models;

namespace VectorDrawPRO.Code.ViewModels;

public class CreateRectangleCommand : ICommand
{
    private readonly Canvas _canvas;

    public CreateRectangleCommand() { }

    public CreateRectangleCommand(Canvas canvas)
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

            Rectangle rectangle = new Rectangle
            {
                X = Convert.ToInt32(mousePosition.X),
                Y = Convert.ToInt32(mousePosition.Y),
                Width = 100,
                Height = 75
            };

            rectangle.Draw(canvas);
        }
    }

    public event EventHandler CanExecuteChanged;
}