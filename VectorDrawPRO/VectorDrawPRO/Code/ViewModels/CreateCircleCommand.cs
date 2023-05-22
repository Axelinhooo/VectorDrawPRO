using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VectorDrawPRO.Code.Models;

namespace VectorDrawPRO.Code.ViewModels
{
    public class CreateCircleCommand : ICommand
    {
        private readonly Canvas canvas;
        public static bool IsSelected = false;

        public CreateCircleCommand(Canvas canvas)
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
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

                Circle circle = new Circle(
                    Convert.ToInt32(mousePosition.X),
                    Convert.ToInt32(mousePosition.Y),
                    width: 100,
                    height: 75,
                    radius: 50
                );

                circle.Draw(canvas);
                IsSelected = true;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}