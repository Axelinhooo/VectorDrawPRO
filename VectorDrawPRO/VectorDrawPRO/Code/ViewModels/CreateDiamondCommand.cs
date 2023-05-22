using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VectorDrawPRO.Code.Models;
using VectorDrawPRO.Code.Models.VectorDrawPRO.Code.Models;

namespace VectorDrawPRO.Code.ViewModels;

public class CreateDiamondCommand : ICommand
{
    private readonly Canvas _canvas;
    public static bool _isSelected = false;

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
                X = Convert.ToInt32(mousePosition.X) - 50,
                Y = Convert.ToInt32(mousePosition.Y) - 50,
                Width = 100,
                Height = 100
            };

            rectangle.Draw(canvas);
        }
    }


    public event EventHandler CanExecuteChanged;
}