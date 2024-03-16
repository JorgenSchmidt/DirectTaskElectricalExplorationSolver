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
        /// Значение кажущегося сопротивления в точке наблюдения
        /// </summary>
        public double Resistance;
        /// <summary>
        /// Значение КС с учётом эта-составляющей
        /// </summary>
        public double Resistance_e;
        /// <summary>
        /// Поляризуемость
        /// </summary>
        public double Polarzability;
    }
}