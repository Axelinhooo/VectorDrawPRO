using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public abstract class Shapes
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        private bool individualEditmode;

        private static readonly List<ShapeMemento> undoStack = new List<ShapeMemento>();
        private static readonly List<ShapeMemento> redoStack = new List<ShapeMemento>();

        public static bool EditMode { get; set; } = false;
        public static bool EraserMode = false;
        public static MenuItem EditShapeMenuItem;
        public static MenuItem SaveShapeMenuItem;
        public static Shape SelectedShape;

        public abstract void Draw(Canvas canvas);

        public void AddMouseLeftButtonDownEvent(Shape shape, Canvas canvas)
        {
            canvas.MouseLeftButtonDown += (sender, e) =>
            {
                if (!EraserMode)
                {
                    if (IsMouseOver(e.GetPosition(canvas)))
                    {
                        if (individualEditmode == false && EditMode == false)
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
                            if (undoStack.Count > 0)
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
                        removeMemento(shape);
                        redoStack.Add(new ShapeMemento(shape, shape.Fill, shape.Stroke, shape.StrokeThickness));
                    }
                }
            };
        }

        public bool IsMouseOver(Point mousePosition)
        {
            if (mousePosition.X >= X && mousePosition.X <= X + Width && mousePosition.Y >= Y &&
                mousePosition.Y <= Y + Height)
            {
                return true;
            }

            return false;
        }

        public ShapeMemento getLatestMemento()
        {
            return undoStack.LastOrDefault();
        }
        
        public static void removeMemento(Shape shape)
        {
            undoStack.Remove(undoStack.FirstOrDefault(x => x.Shape == shape));
        }

        public static void addMemento(Shape shape)
        {
            undoStack.Add(new ShapeMemento(shape, shape.Fill, shape.Stroke, shape.StrokeThickness));
        }

        public static void Undo(Canvas canvas)
        {
            if (undoStack.Count > 0)
            {
                ShapeMemento lastform = undoStack.LastOrDefault();

                if (lastform.Shape.Fill == null)
                {
                    redoStack.Add(lastform);
                    canvas.Children.Remove(lastform.Shape);
                    EditMode = false;
                    EditShapeMenuItem.Visibility = Visibility.Collapsed;
                    SaveShapeMenuItem.Visibility = Visibility.Collapsed;
                    undoStack.Remove(lastform);
                }
                else
                {
                    redoStack.Add(lastform);
                    
                    lastform.Shape.Fill = lastform.Fill;
                    lastform.Shape.Stroke = lastform.Stroke;
                    lastform.Shape.StrokeThickness = lastform.StrokeThickness;
                    
                    undoStack.Remove(lastform);
                }
            }
        }

        public static void Redo(Canvas canvas)
        {
            if (redoStack.Count > 0)
            {
                ShapeMemento lastform = redoStack.LastOrDefault();

                if (!canvas.Children.Contains(lastform.Shape))
                {
                    undoStack.Add(lastform);
                    canvas.Children.Add(lastform.Shape);
                    redoStack.Remove(lastform);
                }
                else
                {
                    lastform.Shape.Fill = lastform.Fill;
                    lastform.Shape.Stroke = lastform.Stroke;
                    lastform.Shape.StrokeThickness = lastform.StrokeThickness;

                    redoStack.Remove(lastform);
                    undoStack.Add(lastform);
                }
            }
        }
    }
}