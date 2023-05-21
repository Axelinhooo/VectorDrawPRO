using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VectorDrawPRO.Code.ViewModels;
using Shape = VectorDrawPRO.Code.Models.Shape;

namespace VectorDrawPRO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MenuItem itemSelected;
        
        public MainWindow()
        {
            InitializeComponent();
            
            canvas.MouseDown += Canvas_MouseDown;
        }
        
        private bool checkIfOneButtonIsSelected()
        {
            if (CreateCircleCommand._isSelected || CreateRectangleCommand._isSelected || CreateTriangleCommand._isSelected || CreateDiamondCommand._isSelected)
            {
                return true;
            }
            return false;
            
        }

        private void selectOne(ICommand shape, object sender)
        {
            CreateCircleCommand._isSelected = shape is CreateCircleCommand ? !CreateCircleCommand._isSelected : false ;
            CreateRectangleCommand._isSelected = shape is CreateRectangleCommand ? !CreateRectangleCommand._isSelected : false;
            CreateTriangleCommand._isSelected = shape is CreateTriangleCommand ? !CreateTriangleCommand._isSelected : false;
            CreateDiamondCommand._isSelected = shape is CreateDiamondCommand ? !CreateDiamondCommand._isSelected : false;
            Cursor = checkIfOneButtonIsSelected() ? Cursors.Pen : Cursors.Arrow;
            Shape.eraserMode = false;
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
        
        private void selectRectangleCommand(object sender,RoutedEventArgs e)
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

        private void selectEraserCommand(object sender, RoutedEventArgs e)
        {
            Shape.eraserMode = !Shape.eraserMode;
            Cursor = Shape.eraserMode ? Cursors.Cross : Cursors.Arrow;
            MenuItem menuItem = sender as MenuItem;
            menuItem.Style = (Style)FindResource("SelectedMenuItemStyle");
        }
                
        private void exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Shape.EditMode == false && Shape.eraserMode == false)
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
    }
}