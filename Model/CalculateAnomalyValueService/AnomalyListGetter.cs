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
            double HostPolarzability,
            double SphereResistance,
            double SpherePolarzability,
            double Amperage,
            // Конфигурация вычислений
            bool CalculateMediavalPoint,
            bool EtaMoreThanZero
        )
        {
            FullAnomalyValues Answer = new FullAnomalyValues() 
            { 
                Values = new List<FullAnomalyValue>()
            };

            // Левый электрод-источник
            double A = StartProfilePoint;
            // Левый регистрирующий электрод
            double M = Math.Round(A + SourseReadStepCount * StepByProfile, 4);
            // Правый регистрирующий электрод
            double N = Math.Round(M + StepByProfile, 4);
            // Правый электрод-источник
            double B = Math.Round(N + SourseReadStepCount * StepByProfile, 4);
            // Показатель L
            double L = Math.Sqrt(Math.Pow(A - (M+N)/2 ,2));
            // Коэффициент установки
            double InstallationKoefficient = 2 * Math.PI * (Math.Pow(L,2) - Math.Pow(StepByProfile/2,2))/(2*(StepByProfile/2));

            // Дополнительные значения для расчёта Эта-составляющей
            double Ro1 = (HostResistance)   / (1 - HostPolarzability);
            double Ro2 = (SphereResistance) / (1 - SpherePolarzability);
            
            // Расчёт значений
            while (B <= EndProfilePoint)
            {
                #region Расчёт основного значения (КС без учёта Эта-составляющей)
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
                #endregion

                #region Расчёт дополнительного значения (КС с учётом Эта-составляющей)
                var valWithPolarzabilityLeft = AnomalyValueCalculator.CalculateValueForThreeElectrodeInstallation(
                    A,
                    M,
                    N,
                    SphereDepth,
                    SphereRadius,
                    Ro1,
                    Ro2,
                    Amperage,
                    InstallationKoefficient,
                    true
                );
                var valWithPolarzabilityRight = AnomalyValueCalculator.CalculateValueForThreeElectrodeInstallation(
                    B,
                    M,
                    N,
                    SphereDepth,
                    SphereRadius,
                    Ro1,
                    Ro2,
                    Amperage,
                    InstallationKoefficient,
                    false
                );
                #endregion

                var resistance = (valLeft + valRight) / 2;
                var resistance_e = (valWithPolarzabilityLeft + valWithPolarzabilityRight) / 2;
                var polarzability = ((resistance_e - resistance) / resistance_e) * 100;

                if (EtaMoreThanZero)
                {
                    if (CalculateMediavalPoint && (A + L == 0))
                    {
                        Answer.Values.Add(
                        new FullAnomalyValue()
                        {
                            X = A + L,
                            H = L,
                            Resistance = 0
                        }
                        );
                    }
                    else
                    {
                        Answer.Values.Add(
                        new FullAnomalyValue()
                        {
                            X = A + L,
                            H = L,
                            Resistance = resistance,
                            Resistance_e = resistance_e,
                            Polarzability = polarzability
                        }
                        );
                    }
                }
                else
                {
                    if (CalculateMediavalPoint && (A + L == 0))
                    {
                        Answer.Values.Add(
                        new FullAnomalyValue()
                        {
                            X = A + L,
                            H = L,
                            Resistance = 0
                        }
                        );
                    }
                    else
                    {
                        Answer.Values.Add(
                            new FullAnomalyValue()
                            {
                                X = A + L,
                                H = L,
                                Resistance = resistance
                            }
                        );
                    }
                }

                A = Math.Round(A + StepByProfile, 4);
                M = Math.Round(M + StepByProfile, 4);
                N = Math.Round(N + StepByProfile, 4);
                B = Math.Round(B + StepByProfile, 4);
            }

            return Answer;
        }
    }
}