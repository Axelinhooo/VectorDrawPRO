using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VectorDrawPRO.Code.Models;

namespace VectorDrawPRO.Code.ViewModels
{
    public class CreateDiamondCommand : ICommand
    {
        private readonly Canvas canvas;
        public static bool IsSelected = false;

        public CreateDiamondCommand(Canvas canvas)
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

                Diamond diamond = new Diamond(
                    Convert.ToInt32(mousePosition.X) - 50,
                    Convert.ToInt32(mousePosition.Y) - 50,
                    width: 100,
                    height: 100
                );

                diamond.Draw(canvas);
                IsSelected = true;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}