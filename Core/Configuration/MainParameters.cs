namespace Core.Configuration
{
    public class MainParameters
    {
        #region Параметры системы наблюдений
        /// <summary>
        /// Значение количества пикетов по умолчанию
        /// </summary>
        public static int PicketCount = 12;
        /// <summary>
        /// Значение половины расстояния между электродами MN по умолчанию
        /// </summary>
        public static double HalfDistanceBetweenMN = 2;
        /// <summary>
        /// Значение силы тока в системе наблюдений по умолчанию
        /// </summary>
        public static double AmperageStrength = 1;
        #endregion

        #region Параметры исследуемой среды
        /// <summary>
        /// Значение сопротивления среды по умолчанию
        /// </summary>
        public static double HostResistance = 1;
        /// <summary>
        /// Значение сопротивления шара по умолчанию
        /// </summary>
        public static double SphereResistance = 0.1;
        /// <summary>
        /// Значение поляризуемости среды по умолчанию
        /// </summary>
        public static double HostPolarzability = 0.02;
        /// <summary>
        /// Значение поляризуемости шара по умолчанию
        /// </summary>
        public static double SpherePolarzability = 0.2;
        #endregion

        #region Параметры геометрии исследуемой среды
        /// <summary>
        /// Значение глубины залегания центра шара по умолчанию
        /// </summary>
        public static double SphereDepth = 10;
        /// <summary>
        /// Значение радиуса шара по умолчанию
        /// </summary>
        public static double SphereRadius = 4;
        #endregion
    }
}