using Core.Entities;

namespace Model.CalculateAnomalyValueService
{
    public class AnomalyDescriptionGetter
    {
        /// <summary>
        /// Генерирует списки профилей с координатами и соответствующими значениями
        /// </summary>
        public static Description GetMainAnomalyDescription (

            // Конфигурация профиля наблюдений
            double PicketCount,
            double StartProfilePoint,
            double EndProfilePoint,
            double StepByProfile,

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
            bool CalculateMediavalPoint
        ) 
        {
            Description Answer = new Description()
            {
                AnomalyObjects = new List<FullAnomalyValues>()
            };

            // Переменная для определения больше ли входные эта-показатели нулю
            bool EtaMoreThanZero = HostPolarzability > 0 && SpherePolarzability > 0;

            for (int SourseReadStepCount = 1; SourseReadStepCount <= (PicketCount - 2)/2; SourseReadStepCount += 1)
            {
                Answer.AnomalyObjects.Add(

                    AnomalyListGetter.GetAnomalyValuesOnProfile(

                        // Конфигурация профиля наблюдений
                        StartProfilePoint, 
                        EndProfilePoint, 
                        StepByProfile, 
                        SourseReadStepCount, 

                        // Геометрия моделируемой среды
                        SphereDepth, 
                        SphereRadius, 

                        // Геофизика моделируемой среды
                        HostResistance, 
                        HostPolarzability,
                        SphereResistance, 
                        SpherePolarzability,
                        Amperage,

                        // Конфигурация вычислений
                        CalculateMediavalPoint,
                        EtaMoreThanZero
                     )
                );
            }

            Answer.MediavalPointWasCalculate = CalculateMediavalPoint;
            Answer.AddiveNotNull = EtaMoreThanZero;

            return Answer; 
        }

    }
}