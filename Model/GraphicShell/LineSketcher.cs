using Core.Entities;
using Core.Entities.GraphicShellEntities;
using System.Windows.Media;

namespace Model.GraphicShell
{
    public class LineSketcher
    {
        public static List<Line> DrawLines( Description AnomalyDescription)
        {
            //TextLabenew Line() { X1 = 50, Y1 = 50, X2 = 950, Y2 = 50, Color = Brushes.Black, StrokeThicknessValue = 1 }
            List<Line> Answer = new List<Line>();

            Answer.Add( new Line () { X1 = 50, Y1 = 50, X2 = GraphicShellConfiguration.CanvasWidth - 50, Y2 = 50, Color = Brushes.Red, StrokeThicknessValue = 2} );

            return Answer;
        }
    }
}