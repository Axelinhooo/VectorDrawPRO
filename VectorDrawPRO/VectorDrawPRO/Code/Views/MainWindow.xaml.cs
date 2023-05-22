using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VectorDrawPRO.Code.Models;
using VectorDrawPRO.Code.ViewModels.Commands;
using VectorDrawPRO.Code.Views;
using Xceed.Wpf.Toolkit;

namespace VectorDrawPRO
{
    public partial class MainWindow : Window
    {
        private MenuItem _selectedMenuItem;
        public ColorPicker ColorPicker { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            canvas.MouseDown += OnCanvasMouseDown;
            Shapes.EditShapeMenuItem = EditShapeMenuItem;
            Shapes.SaveShapeMenuItem = SaveShapeMenuItem;

            KeyDown += OnMainWindowKeyDown;
        }

        private bool CheckIfOneButtonIsSelected()
        {
            return CreateCircleCommand.IsSelected || CreateRectangleCommand.IsSelected ||
                   CreateTriangleCommand.IsSelected || CreateDiamondCommand.IsSelected;
        }

        private void SelectOne(ICommand shape, object sender)
        {
            CreateCircleCommand.IsSelected = shape is CreateCircleCommand ? !CreateCircleCommand.IsSelected : false;
            CreateRectangleCommand.IsSelected = shape is CreateRectangleCommand ? !CreateRectangleCommand.IsSelected : false;
            CreateTriangleCommand.IsSelected = shape is CreateTriangleCommand ? !CreateTriangleCommand.IsSelected : false;
            CreateDiamondCommand.IsSelected = shape is CreateDiamondCommand ? !CreateDiamondCommand.IsSelected : false;
            Cursor = CheckIfOneButtonIsSelected() ? Cursors.Pen : Cursors.Arrow;
            Shapes.EraserMode = false;
            ResetMenuItemStyles();
            var menuItem = sender as MenuItem;
            menuItem.Style = (Style)FindResource("SelectedMenuItemStyle");
            _selectedMenuItem = menuItem;
        }

        private void ResetMenuItemStyles()
        {
            if (_selectedMenuItem != null)
            {
                _selectedMenuItem.Style = null;
            }
        }

        private void SelectCircleCommand(object sender, RoutedEventArgs e)
        {
            SelectOne(new CreateCircleCommand(canvas), sender);
        }

        private void SelectRectangleCommand(object sender, RoutedEventArgs e)
        {
            SelectOne(new CreateRectangleCommand(canvas), sender);
        }

        private void SelectTriangleCommand(object sender, RoutedEventArgs e)
        {
            SelectOne(new CreateTriangleCommand(canvas), sender);
        }

        private void SelectDiamondCommand(object sender, RoutedEventArgs e)
        {
            SelectOne(new CreateDiamondCommand(canvas), sender);
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            Shapes.Undo(canvas);
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            Shapes.Redo(canvas);
        }

        private void SelectEraserCommand(object sender, RoutedEventArgs e)
        {
            Shapes.EraserMode = !Shapes.EraserMode;
            Cursor = Shapes.EraserMode ? Cursors.Cross : Cursors.Arrow;
            ResetMenuItemStyles();
            var menuItem = sender as MenuItem;
            menuItem.Style = (Style)FindResource("SelectedMenuItemStyle");
            _selectedMenuItem = menuItem;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ChangeFill(object sender, RoutedEventArgs e)
        {
            if ((Color)fillPicker.SelectedColor != null)
            {
                Shapes.SelectedShape.Fill = new SolidColorBrush((Color)fillPicker.SelectedColor);
                SaveShapeMenuItem.Visibility = Visibility.Visible;
            }
        }

        private void ChangeStroke(object sender, RoutedEventArgs e)
        {
            if ((Color)strokePicker.SelectedColor != null)
            {
                Shapes.SelectedShape.Stroke = new SolidColorBrush((Color)strokePicker.SelectedColor);
                SaveShapeMenuItem.Visibility = Visibility.Visible;  
            }
        }

        private void ChangeStrokeThickness(object sender, RoutedEventArgs e)
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
            var newWindow = new MainWindow();
            newWindow.Show();
            Close();
        }

        private void About(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void OnCanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!Shapes.EditMode && !Shapes.EraserMode)
            {
                if (CreateCircleCommand.IsSelected)
                {
                    var command = new CreateCircleCommand(canvas);
                    command.Execute(canvas);
                }
                else if (CreateRectangleCommand.IsSelected)
                {
                    var command = new CreateRectangleCommand(canvas);
                    command.Execute(canvas);
                }
                else if (CreateTriangleCommand.IsSelected)
                {
                    var command = new CreateTriangleCommand(canvas);
                    command.Execute(canvas);
                }
                else if (CreateDiamondCommand.IsSelected)
                {
                    var command = new CreateDiamondCommand(canvas);
                    command.Execute(canvas);
                }
            }
        }

        private void OnMainWindowKeyDown(object sender, KeyEventArgs e)
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