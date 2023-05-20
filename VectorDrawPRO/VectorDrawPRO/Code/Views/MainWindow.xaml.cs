using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VectorDrawPRO.Code.ViewModels;

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
        
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // regarde si les boutons sont sélectionnés, si oui ca fait le .execute de la forme correspondante
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