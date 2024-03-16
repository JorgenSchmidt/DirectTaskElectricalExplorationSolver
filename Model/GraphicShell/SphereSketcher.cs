using Core.Constants;
using Core.Entities;
using Core.Entities.GraphicShellEntities;

namespace Model.GraphicShell
{
    public class SphereSketcher
    {
        public static List<Sphere> DrawSpheres (double ProfileLength, double PicketCount, double SphereDepth, double SphereRadius, Description AnomalyDescription)
        {
            List<Sphere> Answer = new List<Sphere>();
            
            // Нахождение максимального значения глубины среди точек наблюдения
            var MaxDepth = Double.MinValue;
            foreach (var l in AnomalyDescription.AnomalyObjects[AnomalyDescription.AnomalyObjects.Count - 1].Values)
            {
                if (l.H > MaxDepth) MaxDepth = l.H;
            }

            // Нахождение диаметра шара
            double SphereDiameter = SphereRadius * 2;

            // Поправочные коэффициенты на размер полотна отрисовки элементов
            var TranslateKoefficient_X = (GraphicShellConfiguration.CanvasWidth - 100) / ProfileLength;
            var TranslateKoefficient_Y = (GraphicShellConfiguration.CanvasHeight - 150) / MaxDepth;

            // Расчёт координат и геометрии для точек наблюдения
            foreach (var Profile in AnomalyDescription.AnomalyObjects)
            {
                foreach (var Value in Profile.Values)
                {
                    Answer.Add(
                        new Sphere
                        {
                            X = Convert.ToInt32((Value.X) * TranslateKoefficient_X) + 500 - Convert.ToInt32(100 / PicketCount)/2,
                            Y = Convert.ToInt32(Value.H * TranslateKoefficient_Y) + 50 - Convert.ToInt32(100 / PicketCount)/2,
                            Color = GraphicsConstants.ExplorationPoints,
                            Radius = Convert.ToInt32(100 / PicketCount) + 1,
                            StrokeThicknessValue = 1,
                            AnomalyValueOnPoint = Value.Resistance,
                            CanBeSigned = true
                        }
                    );
                }
            }

            // Расчёт координат и геометрии для точек-пикетов
            for (int i = 0; i < PicketCount; i++)
            {
                Answer.Add(
                    new Sphere()
                    {
                        X = 50 - Convert.ToInt32(100 / PicketCount) / 2 + Convert.ToInt32((i * (ProfileLength / (PicketCount - 1))) * TranslateKoefficient_X),
                        Y = 50 - Convert.ToInt32(100 / PicketCount) / 2,
                        Color = GraphicsConstants.Pickets,
                        Radius = Convert.ToInt32(100 / PicketCount) + 1,
                        StrokeThicknessValue = 1,
                        CanBeSigned = false
                    }
                );
            }

            // Расчёт координат и геометрии для моделируемого шара
            Answer.Add(
                new Sphere ()
                {
                    X = 500 - Convert.ToInt32(SphereDiameter * TranslateKoefficient_Y)/2,
                    Y = Convert.ToInt32(SphereDepth * TranslateKoefficient_Y) + 50 - Convert.ToInt32(SphereDiameter * TranslateKoefficient_Y) / 2,
                    Color = GraphicsConstants.Sphere,
                    Radius = Convert.ToInt32(SphereDiameter * TranslateKoefficient_Y) + 1,
                    StrokeThicknessValue = 2,
                    CanBeSigned = false
                }    
            );

            return Answer;
        }
    }
}