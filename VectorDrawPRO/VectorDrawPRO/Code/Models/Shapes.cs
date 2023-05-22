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
    public static List<ShapeMemento> undoStack = new List<ShapeMemento>();
    public static List<ShapeMemento> redoStack = new List<ShapeMemento>();

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
                        setMemento(shape);
                        shape.Fill = modified? shape.Fill : Brushes.Transparent;
                        individualEditmode = false;
                        EditMode = false;
                        EditShapeMenuItem.Visibility = Visibility.Collapsed;
                        modified = false;
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
    
    public bool IsMouseOver(Point mousePosition)
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
    
    public void setMemento(Shape shape)
    {
        memento = new ShapeMemento(shape ,SelectedShape.Fill, SelectedShape.Stroke, SelectedShape.StrokeThickness); 
        undoStack.Add(memento);
    }
    
    public static void Undo(Canvas canvas)
    {
        // prend la dernière forme de UndoStack
        if (undoStack.Count > 0)
        {
            if (undoStack[undoStack.Count - 1].Shape.Fill == null)
            {
               redoStack.Add(undoStack[undoStack.Count - 1]);
               canvas.Children.Remove(undoStack[undoStack.Count - 1].Shape);
               undoStack.RemoveAt(undoStack.Count - 1);
            }
            else
            {
                //push dans le redoStack la dernière forme de UndoStack
                redoStack.Add(memento);
    
                // Restaurer l'état précédent de UndoStack
                memento.Shape.Fill = memento.Fill;
                memento.Shape.Stroke = memento.Stroke;
                memento.Shape.StrokeThickness = memento.StrokeThickness;
                
                // enlever la dernière forme de UndoStack
                undoStack.RemoveAt(undoStack.Count - 1);
                
                // attribuer memento à la dernière forme de UndoStack si elle existe
                if (undoStack.Count > 0)
                {
                   memento = undoStack[undoStack.Count - 1]; 
                }
            }
            
        }
    }
    
    public static void Redo(Canvas canvas)
    {
        if (redoStack.Count > 0)
        { 
            if (redoStack[redoStack.Count - 1].Shape.Fill == null)
            {
                undoStack.Add(redoStack[redoStack.Count - 1]);
                canvas.Children.Add(redoStack[redoStack.Count - 1].Shape);
                redoStack.RemoveAt(redoStack.Count - 1);
            }
            else
            {
               //reprend la dernière forme de redoStack
               ShapeMemento lastform = redoStack[redoStack.Count - 1];
   
               // Restaurer l'état précédent de redoStack
               lastform.Shape.Fill = memento.Fill;
               lastform.Shape.Stroke = memento.Stroke;
               lastform.Shape.StrokeThickness = memento.StrokeThickness;
               
               //enlever la dernière forme de redoStack
               redoStack.RemoveAt(redoStack.Count - 1); 
            }
        }
    }

}
