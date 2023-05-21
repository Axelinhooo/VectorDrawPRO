using System.Windows;
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
        public MainWindow()
        {
            InitializeComponent();
            
            canvas.MouseDown += Canvas_MouseDown;
        }
        
        private void selectCircleCommand(object sender, RoutedEventArgs e)
        {
            // change le bouton sélectionné
            CreateCircleCommand._isSelected = true;
            CreateRectangleCommand._isSelected = false;
            CreateTriangleCommand._isSelected = false;
            CreateDiamondCommand._isSelected = false;
        }
        
        private void selectRectangleCommand(object sender, RoutedEventArgs e)
        {
            // change le bouton sélectionné
            CreateCircleCommand._isSelected = false;
            CreateRectangleCommand._isSelected = true;
            CreateTriangleCommand._isSelected = false;
            CreateDiamondCommand._isSelected = false;
        }
        
        private void selectTriangleCommand(object sender, RoutedEventArgs e)
        {
            // change le bouton sélectionné
            CreateCircleCommand._isSelected = false;
            CreateRectangleCommand._isSelected = false;
            CreateTriangleCommand._isSelected = true;
            CreateDiamondCommand._isSelected = false;
        }
        
        private void selectDiamondCommand(object sender, RoutedEventArgs e)
        {
            // change le bouton sélectionné
            CreateCircleCommand._isSelected = false;
            CreateRectangleCommand._isSelected = false;
            CreateTriangleCommand._isSelected = false;
            CreateDiamondCommand._isSelected = true;
        }
        
        private void selectEraserCommand(object sender, RoutedEventArgs e)
        {
            Shape.eraserMode = !Shape.eraserMode;
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