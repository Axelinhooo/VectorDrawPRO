﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using VectorDrawPRO.Code.Models;
using VectorDrawPRO.Code.ViewModels;

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
            Shapes.EditShapeMenuItem = EditShapeMenuItem;
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
            Shapes.SelectedShape.Fill = Brushes.Orange;
        }
        
        private void changeStroke(object sender, RoutedEventArgs e)
        {
            Shapes.SelectedShape.Stroke = Brushes.Aqua;
        }
        
        private void changeStrokeThickness(object sender, RoutedEventArgs e)
        { 
            Shapes.SelectedShape.StrokeThickness = 3;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Shapes.EditMode == false && Shapes.EraserMode == false)
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