using Core.Entities;

namespace Model.CalculateAnomalyValueService
{
    /// <summary>
    /// Содержит методы формирования списков объектов с координатами точки наблюдения и значением кажущегося сопротивления на ней
    /// </summary>
    public class AnomalyListGetter
    {
        /// <summary>
        /// Формирует список значений по формируемому профилю (профилей может быть несколько, в зависимости от конфигурации электроразведочной косы)
        /// </summary>
        public static FullAnomalyValues GetAnomalyValuesOnProfile(
            // Конфигурация профиля наблюдений
            double StartProfilePoint,
            double EndProfilePoint,
            double StepByProfile,
            double SourseReadStepCount,
            // Геометрия моделируемой среды
            double SphereDepth,
            double SphereRadius,
            // Геофизика моделируемой среды
            double HostResistance,
            double SphereResistance,
            double Amperage
        )
        {
            FullAnomalyValues Answer = new FullAnomalyValues() 
            { 
                Values = new List<FullAnomalyValue>()
            };

            // Левый электрод-источник
            double A = StartProfilePoint;
            // Левый регистрирующий электрод
            double M = A + SourseReadStepCount*StepByProfile;
            // Правый регистрирующий электрод
            double N = M + StepByProfile;
            // Правый электрод-источник
            double B = N + SourseReadStepCount * StepByProfile;
            // Показатель L
            double L = Math.Sqrt(Math.Pow(A - (M+N)/2 ,2));
            // Коэффициент установки
            double InstallationKoefficient = 2 * Math.PI * (Math.Pow(L,2) - Math.Pow(StepByProfile/2,2))/(2*(StepByProfile/2));

            // Расчёт значений
            while (B <= EndProfilePoint)
            {
                // Расчёт значения для левой трёхэлектродной установки
                var valLeft = AnomalyValueCalculator.CalculateValueForThreeElectrodeInstallation(
                    A,
                    M,
                    N,
                    SphereDepth,
                    SphereRadius,
                    HostResistance,
                    SphereResistance,
                    Amperage,
                    InstallationKoefficient,
                    true
                );
                // Расчёт значения для правой трёхэлектродной установки
                var valRight = AnomalyValueCalculator.CalculateValueForThreeElectrodeInstallation(
                    B,
                    M,
                    N,
                    SphereDepth,
                    SphereRadius,
                    HostResistance,
                    SphereResistance,
                    Amperage,
                    InstallationKoefficient,
                    false
                );
                Answer.Values.Add(
                    new FullAnomalyValue() 
                    { 
                        X = A + L , 
                        H = L,
                        Value = (valLeft + valRight)/2
                    }
                );
                A += StepByProfile;
                M += StepByProfile;
                N += StepByProfile;
                B += StepByProfile;
            }

            return Answer;
        }
    }
}