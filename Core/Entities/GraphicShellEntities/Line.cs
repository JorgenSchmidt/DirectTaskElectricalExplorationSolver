using System.Windows.Media;

namespace Core.Entities.GraphicShellEntities
{
    public class Line
    {
        // Координаты ключевых точек линии
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

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