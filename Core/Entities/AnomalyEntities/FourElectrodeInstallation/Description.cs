namespace Core.Entities
{
    public class Description
    {
        /// <summary>
        /// Описание измеряемой аномалии, возможно время и место проведения
        /// </summary>
        public string AnomalyDescription;
        /// <summary>
        /// Значение измеренной аномалии по всем профилям
        /// </summary>
        public List<FullAnomalyValues> AnomalyObjects;
        /// <summary>
        /// Определяет было ли рассчитано среднее значение
        /// </summary>
        public bool MediavalPointWasCalculate;
        /// <summary>
        /// Определяет равна ли Эта-составляющая 0 для одного из значений
        /// </summary>
        public bool AddiveNotNull;
    }
}