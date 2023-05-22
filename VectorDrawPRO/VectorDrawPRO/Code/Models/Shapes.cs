using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorDrawPRO.Code.Models
{
    public abstract class Shapes // Classe abstraite Shapes
    {
        // Propriétés de la classe Shapes
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        private bool individualEditmode;

        private static readonly List<ShapeMemento> undoStack = new List<ShapeMemento>(); // Liste des mementos
        private static readonly List<ShapeMemento> redoStack = new List<ShapeMemento>();
        
        public static bool EditMode { get; set; } = false;
        public static bool EraserMode = false;
        public static MenuItem EditShapeMenuItem;
        public static MenuItem SaveShapeMenuItem;
        public static Shape SelectedShape;

        // Méthodes de la classe Shapes
        public abstract void Draw(Canvas canvas);

        public void AddMouseLeftButtonDownEvent(Shape shape, Canvas canvas) // Ajout de l'évènement MouseLeftButtonDown à la forme qui permet de la sélectionner
        {
            canvas.MouseLeftButtonDown += (sender, e) => 
            {
                if (!EraserMode)
                {
                    if (IsMouseOver(e.GetPosition(canvas))) // Si la souris est sur la forme
                    {
                        if (individualEditmode == false && EditMode == false) // Si on est pas en mode édition
                        {
                            SelectedShape = shape;
                            addMemento(shape);
                            shape.Fill = Brushes.Red;
                            individualEditmode = true;
                            EditMode = true;
                            EditShapeMenuItem.Visibility = Visibility.Visible; // On affiche le menu d'édition
                        }
                    }
                    else
                    {
                        if (individualEditmode) // Si on est en mode édition
                        {
                            if (undoStack.Count > 0)
                            {
                                SelectedShape.Fill = getLatestMemento().Fill; // On récupère le dernier memento de la forme
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
                        canvas.Children.Remove(shape); // On supprime la forme
                        removeMemento(shape); // On supprime le memento de la forme
                        redoStack.Add(new ShapeMemento(shape, shape.Fill, shape.Stroke, shape.StrokeThickness)); // On ajoute le memento à la liste des mementos
                    }
                }
            };
        }

        public bool IsMouseOver(Point mousePosition) // Vérifie si la souris est sur la forme
        {
            if (mousePosition.X >= X && mousePosition.X <= X + Width && mousePosition.Y >= Y &&
                mousePosition.Y <= Y + Height)
            {
                return true;
            }

            return false;
        }

        public ShapeMemento getLatestMemento() // Récupère le dernier memento de la forme
        {
            return undoStack.LastOrDefault();
        }
        
        public static void removeMemento(Shape shape) // Supprime le memento de la forme
        {
            undoStack.Remove(undoStack.FirstOrDefault(x => x.Shape == shape));
        }

        public static void addMemento(Shape shape) // Ajoute le memento de la forme
        {
            undoStack.Add(new ShapeMemento(shape, shape.Fill, shape.Stroke, shape.StrokeThickness));
        }

        public static void Undo(Canvas canvas) // Annule la dernière action
        {
            if (undoStack.Count > 0)
            {
                ShapeMemento lastform = undoStack.LastOrDefault();

                if (lastform.Shape.Fill == null)
                {
                    redoStack.Add(lastform); // On ajoute le memento à la liste des mementos
                    canvas.Children.Remove(lastform.Shape); // On supprime la forme
                    EditMode = false;
                    EditShapeMenuItem.Visibility = Visibility.Collapsed;
                    SaveShapeMenuItem.Visibility = Visibility.Collapsed;
                    undoStack.Remove(lastform); // On supprime le memento de la liste des mementos
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

        public static void Redo(Canvas canvas) // Rétablit la dernière action
        {
            if (redoStack.Count > 0)
            {
                ShapeMemento lastform = redoStack.LastOrDefault();

                if (!canvas.Children.Contains(lastform.Shape)) // Si la forme n'est pas dans le canvas
                {
                    undoStack.Add(lastform);
                    canvas.Children.Add(lastform.Shape); // On ajoute la forme
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