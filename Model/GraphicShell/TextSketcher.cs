using Core.Entities.GraphicShellEntities;
using static System.Net.Mime.MediaTypeNames;

namespace Model.GraphicShell
{
    public class TextSketcher
    {
        public static List<TextLabel> WriteLabels(int PicketCount, List<Sphere> Spheres)
        {
            List<TextLabel> Answer = new List<TextLabel>();

            foreach (var CurrentSphere in Spheres)
            {
                if (CurrentSphere.CanBeSigned)
                {
                    Answer.Add(
                        new TextLabel()
                        {
                            X = CurrentSphere.X,
                            Y = CurrentSphere.Y + CurrentSphere.Radius,
                            FontSize = Convert.ToInt32(Math.Round(-0.4 * PicketCount + 23)),
                            Text = Math.Round(CurrentSphere.AnomalyValueOnPoint, 2).ToString(),
                        }
                    );
                }
            }

            return Answer;
        }
    }
}