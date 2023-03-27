using Core.Entities;
using Core.Entities.GraphicShellEntities;
using System.Windows.Media;

namespace Model.GraphicShell
{
    public class SphereSketcher
    {
        public static List<Sphere> DrawSpheres (double ProfileLength, Description AnomalyDescription)
        {
            List<Sphere> Answer = new List<Sphere>();

            // Нахождение максимального значения глубины среди точек наблюдения
            var MaxDepth = Double.MinValue;
            foreach (var l in AnomalyDescription.AnomalyObjects[AnomalyDescription.AnomalyObjects.Count - 1].Values)
            {
                if (l.H > MaxDepth) MaxDepth = l.H;
            }

            /*// Нахождение максимального значения координаты Х среди точек наблюдения
            var MaxCoordinateX = AnomalyDescription.AnomalyObjects[0]
                                .Values[AnomalyDescription.AnomalyObjects[0].Values.Count - 1].X;*/

            // Поправочные коэффициенты на размер полотна отрисовки приложения
            var TranslateKoefficient_X = (GraphicShellConfiguration.CanvasWidth - 100) / ProfileLength;
            var TranslateKoefficient_Y = (GraphicShellConfiguration.CanvasHeight - 100) / MaxDepth;

            foreach (var Profile in AnomalyDescription.AnomalyObjects)
            {
                foreach (var Value in Profile.Values)
                {
                    Answer.Add(
                        new Sphere
                        {
                            X = Convert.ToInt32((Value.X) * TranslateKoefficient_X) + 500 ,
                            Y = Convert.ToInt32(Value.H * TranslateKoefficient_Y) + 50,
                            Color = Brushes.Black,
                            Radius = Convert.ToInt32(100 / ProfileLength) + 1,
                            StrokeThicknessValue = 1,
                        }
                    );
                }
            }

            return Answer;
        }
    }
}