using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
    
    public static MenuItem SaveShapeMenuItem; 
    
    public static Shape SelectedShape;
    
    private bool individualEditmode;
    
    public static bool modified = false;
   
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
                        SelectedShape = shape;
                        addMemento(shape);
                        
                        shape.Fill = Brushes.Red;
                        individualEditmode = true;
                        EditMode = true;
                        EditShapeMenuItem.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (individualEditmode)
                    {
                        shape.Fill = undoStack[undoStack.Count - 1].Fill;
                        shape.Stroke = undoStack[undoStack.Count - 1].Stroke;
                        shape.StrokeThickness = undoStack[undoStack.Count - 1].StrokeThickness;
                        individualEditmode = false;
                        EditMode = false;
                        EditShapeMenuItem.Visibility = Visibility.Collapsed;
                        SaveShapeMenuItem.Visibility = Visibility.Collapsed;
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
    
    public static void addMemento(Shape shape)
    {
        undoStack.Add(new ShapeMemento(shape ,SelectedShape.Fill, SelectedShape.Stroke, SelectedShape.StrokeThickness));
    }
    
    public static void Undo(Canvas canvas)
    {
        if (undoStack.Count > 0)
        {
            ShapeMemento lastform = undoStack[undoStack.Count - 1];
            if (undoStack[undoStack.Count - 1].Shape.Fill == null)
            {
               redoStack.Add(undoStack[undoStack.Count - 1]);
               canvas.Children.Remove(undoStack[undoStack.Count - 1].Shape);
               undoStack.RemoveAt(undoStack.Count - 1);
            }
            else
            {
                lastform.Shape.Fill = lastform.Fill;
                lastform.Shape.Stroke = lastform.Stroke;
                lastform.Shape.StrokeThickness = lastform.StrokeThickness;
                
                redoStack.Add(lastform);
                undoStack.RemoveAt(undoStack.Count - 1);
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
               ShapeMemento lastform = redoStack[redoStack.Count - 1];
               
               lastform.Shape.Fill = lastform.Fill;
               lastform.Shape.Stroke = lastform.Stroke;
               lastform.Shape.StrokeThickness = lastform.StrokeThickness;
               
               redoStack.RemoveAt(redoStack.Count - 1); 
            }
        }
    }

}
