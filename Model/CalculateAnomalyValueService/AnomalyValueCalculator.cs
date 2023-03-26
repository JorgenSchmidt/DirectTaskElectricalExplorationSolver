namespace Model.CalculateAnomalyValueService
{
    /// <summary>
    /// Содержит методы вычисления зафиксированного кажущегося сопротивления для трёхэлектродной установки
    /// </summary>
    public class AnomalyValueCalculator
    {
        /// <summary>
        /// По некоторым входным параметрам рассчитывает значение кажущегося сопротивления для трёхэлектродной установки
        /// </summary>
        public static double CalculateValueForThreeElectrodeInstallation (
            // Координаты электродов
            double  SourseCoordnate, // Соответствует координатам А или В в зависимости от логического переключателя IsLeftSourse
            double  LeftReadElectrodeCoordinate, 
            double  RightReadElectrodeCoordinate, 
            // Геометрия моделируемой среды
            double  SphereOccurenceDepth, 
            double  SphereRadius, 
            // Геофизика моделируемой среды
            double  HostRocksResistanse, 
            double  SphereResistanse, 
            double  Amperage,
            // Коэффициент установки
            double  InstallationKoefficient,
            // Логические переключатели
            bool    IsLeftSourse
        )
        {
            // Показатель π (нужен для определения того, какое значение соответствует пи (в методическом пособии было 3.14 =>
            // чтоб оперативно меняться между тестированием и расчётом "реальных данных" переменная была вынесена отдельно))
            double PI = Math.PI;

            // Показатель d
            double ObservationAndSourseDistance    = Math.Sqrt(Math.Pow(SourseCoordnate, 2)                + Math.Pow(SphereOccurenceDepth, 2)); 
            
            // Показатель rM
            double ObservationAndLeftReadDistance  = Math.Sqrt(Math.Pow(LeftReadElectrodeCoordinate, 2)    + Math.Pow(SphereOccurenceDepth, 2));
            
            // Показатель rN
            double ObservationAndRightReadDistance = Math.Sqrt(Math.Pow(RightReadElectrodeCoordinate, 2)   + Math.Pow(SphereOccurenceDepth, 2));
           
            // Показатель LM
            double SourseAndLeftReadDistance       = Math.Sqrt(Math.Pow(SourseCoordnate - LeftReadElectrodeCoordinate, 2));
            
            // Показатель LN
            double SourseAndRightReadDistance      = Math.Sqrt(Math.Pow(SourseCoordnate - RightReadElectrodeCoordinate, 2));
           
            // Показатель UM
            double ReadedVoltageValueFromLeft      = ((Amperage * HostRocksResistanse) / (2 * PI))
                                                      * ( (1 / SourseAndLeftReadDistance) 
                                                      + 2 * (HostRocksResistanse - SphereResistanse) / (HostRocksResistanse + 2 * SphereResistanse)
                                                      * (SphereRadius / (ObservationAndLeftReadDistance * ObservationAndSourseDistance)
                                                      - SphereRadius / Math.Sqrt(
                                                            (Math.Pow(ObservationAndSourseDistance, 2) - Math.Pow(SphereRadius, 2))
                                                          * (Math.Pow(ObservationAndLeftReadDistance, 2) - Math.Pow(SphereRadius, 2))
                                                          + (Math.Pow(SphereRadius, 2) * Math.Pow(SourseAndLeftReadDistance, 2)))
                                                      ));
            // Показатель UN
            double ReadedVoltageValueFromRight      = ((Amperage * HostRocksResistanse) / (2 * PI))
                                                      * ((1 / SourseAndRightReadDistance)
                                                      + 2 * (HostRocksResistanse - SphereResistanse) / (HostRocksResistanse + 2 * SphereResistanse)
                                                      * (SphereRadius / (ObservationAndRightReadDistance * ObservationAndSourseDistance)
                                                      - SphereRadius / Math.Sqrt(
                                                            (Math.Pow(ObservationAndSourseDistance, 2) - Math.Pow(SphereRadius, 2))
                                                          * (Math.Pow(ObservationAndRightReadDistance, 2) - Math.Pow(SphereRadius, 2))
                                                          + (Math.Pow(SphereRadius, 2) * Math.Pow(SourseAndRightReadDistance, 2)))
                                                      ));
            // Показатель dU
            double ReadedVoltageValue;
            if (IsLeftSourse)
            {
                ReadedVoltageValue = ReadedVoltageValueFromLeft - ReadedVoltageValueFromRight;
            }
            else
            {
                ReadedVoltageValue = ReadedVoltageValueFromRight - ReadedVoltageValueFromLeft;
            }

            // Расчёт показателя Rok
            double ApparentResistanceValue = InstallationKoefficient * (ReadedVoltageValue/Amperage);

            return ApparentResistanceValue;
        }
    }
}