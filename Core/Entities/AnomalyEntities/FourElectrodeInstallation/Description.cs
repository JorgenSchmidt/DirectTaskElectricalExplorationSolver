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
    }
}