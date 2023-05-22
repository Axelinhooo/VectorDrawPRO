using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorDrawPRO.Code.Models;
using VectorDrawPRO.Code.ViewModels;
using VectorDrawPRO.Code.Views;
using Xceed.Wpf.Toolkit;

namespace VectorDrawPRO
{
    public partial class MainWindow : Window
    {
        MenuItem itemSelected;
        public ColorPicker colorPicker { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            canvas.MouseDown += Canvas_MouseDown;
            Shapes.EditShapeMenuItem = EditShapeMenuItem;

            KeyDown += MainWindow_KeyDown;
        }

        private bool checkIfOneButtonIsSelected()
        {
            return CreateCircleCommand._isSelected || CreateRectangleCommand._isSelected ||
                   CreateTriangleCommand._isSelected || CreateDiamondCommand._isSelected;
        }

        private void selectOne(ICommand shape, object sender)
        {
            CreateCircleCommand._isSelected = shape is CreateCircleCommand ? !CreateCircleCommand._isSelected : false;
            CreateRectangleCommand._isSelected = shape is CreateRectangleCommand ? !CreateRectangleCommand._isSelected : false;
            CreateTriangleCommand._isSelected = shape is CreateTriangleCommand ? !CreateTriangleCommand._isSelected : false;
            CreateDiamondCommand._isSelected = shape is CreateDiamondCommand ? !CreateDiamondCommand._isSelected : false;
            Cursor = checkIfOneButtonIsSelected() ? Cursors.Pen : Cursors.Arrow;
            Shapes.EraserMode = false;
            ResetMenuItemStyles();
            MenuItem menuItem = sender as MenuItem;
            menuItem.Style = (Style)FindResource("SelectedMenuItemStyle");
            itemSelected = menuItem;
        }

        private void ResetMenuItemStyles()
        {
            if (itemSelected != null)
            {
                itemSelected.Style = null;
            }
        }

        private void selectCircleCommand(object sender, RoutedEventArgs e)
        {
            selectOne(new CreateCircleCommand(canvas), sender);
        }

        private void selectRectangleCommand(object sender, RoutedEventArgs e)
        {
            selectOne(new CreateRectangleCommand(canvas), sender);
        }

        private void selectTriangleCommand(object sender, RoutedEventArgs e)
        {
            selectOne(new CreateTriangleCommand(canvas), sender);
        }

        private void selectDiamondCommand(object sender, RoutedEventArgs e)
        {
            selectOne(new CreateDiamondCommand(canvas), sender);
        }

        private void undo(object sender, RoutedEventArgs e)
        {
            Shapes.Undo(canvas);
        }

        private void redo(object sender, RoutedEventArgs e)
        {
            Shapes.Redo(canvas);
        }

        private void selectEraserCommand(object sender, RoutedEventArgs e)
        {
            Shapes.EraserMode = !Shapes.EraserMode;
            Cursor = Shapes.EraserMode ? Cursors.Cross : Cursors.Arrow;
            ResetMenuItemStyles();
            MenuItem menuItem = sender as MenuItem;
            menuItem.Style = (Style)FindResource("SelectedMenuItemStyle");
            itemSelected = menuItem;
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void changeFill(object sender, RoutedEventArgs e)
        {
            Shapes.SelectedShape.Fill = new SolidColorBrush((Color)fillPicker.SelectedColor);
            Shapes.modified = true;
        }

        private void changeStroke(object sender, RoutedEventArgs e)
        {
            Shapes.SelectedShape.Stroke = new SolidColorBrush((Color)strokePicker.SelectedColor);
            Shapes.modified = true;
        }

        private void changeStrokeThickness(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(thicknessBox.Text, out double output))
            {
                Shapes.SelectedShape.StrokeThickness = output;
                Shapes.modified = true;
            }
        }

        private void New(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            Close();
        }

        private void About(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!Shapes.EditMode && !Shapes.EraserMode)
            {
                if (CreateCircleCommand._isSelected)
                {
                    CreateCircleCommand command = new CreateCircleCommand(canvas);
                    command.Execute(canvas);
                }
                else if (CreateRectangleCommand._isSelected)
                {
                    CreateRectangleCommand command = new CreateRectangleCommand(canvas);
                    command.Execute(canvas);
                }
                else if (CreateTriangleCommand._isSelected)
                {
                    CreateTriangleCommand command = new CreateTriangleCommand(canvas);
                    command.Execute(canvas);
                }
                else if (CreateDiamondCommand._isSelected)
                {
                    CreateDiamondCommand command = new CreateDiamondCommand(canvas);
                    command.Execute(canvas);
                }
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.Z)
                {
                    Shapes.Undo(canvas);
                }
                else if (e.Key == Key.Y)
                {
                    Shapes.Redo(canvas);
                }
            }
        }
    }
}
