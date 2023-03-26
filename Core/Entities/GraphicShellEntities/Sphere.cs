using System.Windows.Media;

namespace Core.Entities.GraphicShellEntities
{
    public class Sphere
    {
        public int X { get; set; }
        public int Y { get; set; }
        public object SphereMargin { get; set; }
        public int Radius { get; set; }
        /// <summary>
        /// Ширина линии
        /// </summary>
        public int StrokeThicknessValue { get; set; }
        /// <summary>
        /// Цвет линии
        /// </summary>
        public SolidColorBrush Color { get; set; }

    }
}