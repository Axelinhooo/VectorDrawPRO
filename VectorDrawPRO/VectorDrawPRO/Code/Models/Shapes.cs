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

    public static List<ShapeMemento> undoStack = new List<ShapeMemento>();
    public static List<ShapeMemento> redoStack = new List<ShapeMemento>();

    public abstract void Draw(Canvas canvas);

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
                        if (SelectedShape != null)
                        {
                            SelectedShape.Fill = getLatestMemento().Fill;
                            SelectedShape.Stroke = getLatestMemento().Stroke;
                            SelectedShape.StrokeThickness = getLatestMemento().StrokeThickness;
                        }
                        individualEditmode = false;
                        EditMode = false;
                        EditShapeMenuItem.Visibility = Visibility.Collapsed;
                        SaveShapeMenuItem.Visibility = Visibility.Collapsed;
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
        return false;
    }
    
    public ShapeMemento getLatestMemento()
    {
        return undoStack[undoStack.Count - 1];
    }
    
    public static void addMemento(Shape shape)
    {
        undoStack.Add(new ShapeMemento(shape ,shape.Fill, shape.Stroke, shape.StrokeThickness));
    }
    
    public static void Undo(Canvas canvas)
    {
        if (undoStack.Count > 0)
        {
            ShapeMemento lastform = undoStack[undoStack.Count - 1];
            if (lastform.Shape.Fill == null)
            {
               redoStack.Add(lastform);
               canvas.Children.Remove(lastform.Shape);
               EditMode = false;
               EditShapeMenuItem.Visibility = Visibility.Collapsed;
               SaveShapeMenuItem.Visibility = Visibility.Collapsed;
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
            ShapeMemento lastform = redoStack[redoStack.Count - 1];
            if (lastform.Shape.Fill == null)
            {
                undoStack.Add(lastform);
                canvas.Children.Add(lastform.Shape);
                redoStack.RemoveAt(redoStack.Count - 1);
            }
            else
            {
                lastform.Shape.Fill = lastform.Fill;
                lastform.Shape.Stroke = lastform.Stroke;
                lastform.Shape.StrokeThickness = lastform.StrokeThickness;
               
                redoStack.RemoveAt(redoStack.Count - 1); 
            }
        }
    }

}
