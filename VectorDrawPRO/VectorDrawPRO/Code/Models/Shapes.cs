using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models;

public abstract class Shapes
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public static bool EditMode { get; set; } = false;
    
    public static bool EraserMode = false;

    public static MenuItem EditShapeMenuItem;
    
    public static Shape SelectedShape;
    
    private bool individualEditmode;
    
    public static bool modified = false;
    
    private static ShapeMemento memento;
    private static Stack<ShapeMemento> undoStack = new Stack<ShapeMemento>();
    private static Stack<ShapeMemento> redoStack = new Stack<ShapeMemento>();

    public abstract void Draw(Canvas canvas);

    public void setStrokeColor(Brush color, Shape shape)
    {
        shape.Stroke = color;
    }

    public void AddMouseLeftButtonDownEvent(Shape shape, Canvas canvas)
    {
        canvas.MouseLeftButtonDown += (sender, e) =>
        {
            if (!EraserMode)
            {
                if (IsMouseOver(e.GetPosition(canvas)))
                {
                    if(individualEditmode == false && EditMode == false)
                    {
                        shape.Fill = Brushes.Red;
                        individualEditmode = true;
                        EditMode = true;
                        EditShapeMenuItem.Visibility = Visibility.Visible;
                        SelectedShape = shape;
                    }
                }
                else
                {
                    if (individualEditmode)
                    {

                        shape.Fill = modified? shape.Fill : Brushes.Transparent;
                        individualEditmode = false;
                        EditMode = false;
                        EditShapeMenuItem.Visibility = Visibility.Collapsed;
                        modified = false;
                        
                        memento = new ShapeMemento(shape ,SelectedShape.Fill, SelectedShape.Stroke, SelectedShape.StrokeThickness); 
                        undoStack.Push(memento);
                        
                    }
                }
            }
            else
            {
                if (IsMouseOver(e.GetPosition(canvas)))
                {
                    canvas.Children.Remove(shape);
                }
            }
        };
    }
    
    public bool IsMouseOver(System.Windows.Point mousePosition)
    {
        if (mousePosition.X >= X && mousePosition.X <= X + Width && mousePosition.Y >= Y && mousePosition.Y <= Y + Height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public static void Undo()
    {
        // prend la dernière forme de UndoStack
        if (undoStack.Count > 0)
        {
            //push dans le redoStack la dernière forme de UndoStack
            redoStack.Push(memento);

            // Restaurer l'état précédent de UndoStack
            memento.Shape.Fill = memento.Fill;
            memento.Shape.Stroke = memento.Stroke;
            memento.Shape.StrokeThickness = memento.StrokeThickness;
            
            // enlever la dernière forme de UndoStack
            undoStack.Pop();
            
            // attribuer memento à la dernière forme de UndoStack si elle existe
            if (undoStack.Count > 0)
            {
               memento = undoStack.Peek(); 
            }
        }
    }
    
    public static void Redo()
    {
        if (redoStack.Count > 0)
        { 
            //reprend la dernière forme de redoStack
            ShapeMemento lastform = redoStack.Peek();

            // Restaurer l'état précédent de redoStack
            lastform.Shape.Fill = memento.Fill;
            lastform.Shape.Stroke = memento.Stroke;
            lastform.Shape.StrokeThickness = memento.StrokeThickness;
            
            //enlever la dernière forme de redoStack
            redoStack.Pop();
        }
    }

}
