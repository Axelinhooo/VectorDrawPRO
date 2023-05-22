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
            Shapes.SaveShapeMenuItem = SaveShapeMenuItem;

            KeyDown += MainWindow_KeyDown;
        }

        private bool checkIfOneButtonIsSelected()
        {
            return CreateCircleCommand.IsSelected || CreateRectangleCommand.IsSelected ||
                   CreateTriangleCommand.IsSelected || CreateDiamondCommand.IsSelected;
        }

        private void selectOne(ICommand shape, object sender)
        {
            CreateCircleCommand.IsSelected = shape is CreateCircleCommand ? !CreateCircleCommand.IsSelected : false;
            CreateRectangleCommand.IsSelected = shape is CreateRectangleCommand ? !CreateRectangleCommand.IsSelected : false;
            CreateTriangleCommand.IsSelected = shape is CreateTriangleCommand ? !CreateTriangleCommand.IsSelected : false;
            CreateDiamondCommand.IsSelected = shape is CreateDiamondCommand ? !CreateDiamondCommand.IsSelected : false;
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
            if ((Color)fillPicker.SelectedColor != null)
            {
                Shapes.SelectedShape.Fill = new SolidColorBrush((Color)fillPicker.SelectedColor);
                SaveShapeMenuItem.Visibility = Visibility.Visible;
            }
        }

        private void changeStroke(object sender, RoutedEventArgs e)
        {
            if ((Color)strokePicker.SelectedColor != null)
            {
              Shapes.SelectedShape.Stroke = new SolidColorBrush((Color)strokePicker.SelectedColor);
              SaveShapeMenuItem.Visibility = Visibility.Visible;  
            }
        }

        private void changeStrokeThickness(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(thicknessBox.Text, out double output))
            {
                Shapes.SelectedShape.StrokeThickness = output;
                SaveShapeMenuItem.Visibility = Visibility.Visible;
            }
        }
        
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            SaveShapeMenuItem.Visibility = Visibility.Collapsed;
            Shapes.addMemento(Shapes.SelectedShape);
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
                if (CreateCircleCommand.IsSelected)
                {
                    CreateCircleCommand command = new CreateCircleCommand(canvas);
                    command.Execute(canvas);
                }
                else if (CreateRectangleCommand.IsSelected)
                {
                    CreateRectangleCommand command = new CreateRectangleCommand(canvas);
                    command.Execute(canvas);
                }
                else if (CreateTriangleCommand.IsSelected)
                {
                    CreateTriangleCommand command = new CreateTriangleCommand(canvas);
                    command.Execute(canvas);
                }
                else if (CreateDiamondCommand.IsSelected)
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
                else if (e.Key == Key.S)
                {
                    Save_OnClick(sender, e);
                }
            }
        }
    }
}
