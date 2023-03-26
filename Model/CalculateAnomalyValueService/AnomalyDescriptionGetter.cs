using Core.Entities;

namespace Model.CalculateAnomalyValueService
{
    public class AnomalyDescriptionGetter
    {
        /// <summary>
        /// Генерирует списки профилей с координатами и соответствующими значениями
        /// </summary>
        public static Description GetAnomalyDescription (
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
            double SphereResistance,
            double Amperage
        ) 
        {
            Description Answer = new Description()
            {
                AnomalyObjects = new List<FullAnomalyValues>()
            };

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
                        SphereResistance, 
                        Amperage
                     )
                );
            }

            return Answer; 
        }
    }
}