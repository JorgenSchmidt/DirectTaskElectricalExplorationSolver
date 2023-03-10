namespace Model.CalculateAnomalyValueService
{
    public class AnomalyCalculator
    {
        public static double CalculateElectricAnomaly(double p, double J, double x, double y, double h, double a, double b, double c)
        {
            double V = ((p * J) 
                        / 
                        ( 2 * Math.PI)) 
                        * 
                        ( 1 / ( Math.Sqrt(Math.Pow(x - a, 2) 
                            + Math.Pow(y - b, 2) 
                            + Math.Pow(h - c, 2))));
            return V;
        }
    }
}