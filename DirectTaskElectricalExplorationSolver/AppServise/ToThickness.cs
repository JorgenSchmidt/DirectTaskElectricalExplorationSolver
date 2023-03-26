using System.Windows;

namespace DirectTaskElectricalExplorationSolver.AppServise
{
    public class ToThickness
    {
        public static object Convert(int x, int y)
        {
            return new Thickness(x, y, 0, 0);
        }
    }
}