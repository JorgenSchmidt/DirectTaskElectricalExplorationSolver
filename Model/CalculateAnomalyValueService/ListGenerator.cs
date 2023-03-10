using Core.Entities;

namespace Model.CalculateAnomalyValueService
{
    public class ListGenerator
    {
        public static AnomalyValues GenerateList(double p, double J, double h, double a, double b, double c)
        {
            AnomalyValues description = new AnomalyValues();
            description.Values = new List<AnomalyValue>();
            for (double x = 0; x <= 1000; x += 100)
            {
                for (double y = 0; y <= 1000; y += 100)
                {
                    description.Values.Add(new AnomalyValue()
                    {
                        X = x,
                        Y = y,
                        Value = AnomalyCalculator.CalculateElectricAnomaly(p, J, x, y, h, a, b, c)
                    });
                }

            }
            return description;
        }
    }
}