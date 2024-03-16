using Core.Constants;
using Core.Entities;
using Core.Entities.GraphicShellEntities;

namespace Model.GraphicShell
{
    public class LineSketcher
    {
        public static List<Line> DrawLines( Description AnomalyDescription)
        {
            List<Line> Answer = new List<Line>();

            Answer.Add( 
                new Line () 
                { 
                    X1 = 50, 
                    Y1 = 50, 
                    X2 = GraphicShellConfiguration.CanvasWidth - 50, 
                    Y2 = 50, 
                    Color = GraphicsConstants.LineColor, 
                    StrokeThicknessValue = 2
                } 
            );

            return Answer;
        }
    }
}