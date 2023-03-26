namespace Core.Entities
{
    public class FullAnomalyValue
    {
        /// <summary>
        /// Расположение точки наблюдения по горизонтали
        /// </summary>
        public double X;
        /// <summary>
        /// Расположение точки наблюдения по вертикали (глубина)
        /// </summary>
        public double H;
        /// <summary>
        /// Значение аномалии в точке наблюдения
        /// </summary>
        public double Value;
    }
}