using GFDirectTasksSolver.ViewModelService;

namespace DirectTaskElectricalExplorationSolver.ViewModels
{
    public class AppWindowViewModel : NotifyPropertyChanged
    {
        public int picketCount;

        public int PicketCount
        {
            get
            {
                return picketCount;
            }
            set 
            { 
                picketCount = value; 
                CheckChanges();
            }
        }

        public double halfDistanceBetweenMN;
        public double HalfDistanceBetweenMN
        {
            get
            {
                return halfDistanceBetweenMN;
            }
            set
            {
                halfDistanceBetweenMN = value;
                CheckChanges();
            }
        }

        public double hostResistance;
        public double HostResistance
        {
            get
            {
                return hostResistance;
            }
            set
            {
                hostResistance = value;
                CheckChanges();
            }
        }

        public double sphereResistance;
        public double SphereResistance
        {
            get
            {
                return sphereResistance;
            }
            set
            {
                sphereResistance = value;
                CheckChanges();
            }
        }

        public double amperageStrenght;
        public double AmperageStrength
        {
            get
            {
                return amperageStrenght;
            }
            set
            {
                amperageStrenght = value;
                CheckChanges();
            }
        }

        public double sphereDepth;
        public double SphereDepth
        {
            get
            {
                return sphereDepth;
            }
            set
            {
                sphereDepth = value;
                CheckChanges();
            }
        }

        public double sphereRadius; 
        public double SphereRadius
        {
            get
            {
                return sphereRadius;
            }
            set
            {
                sphereRadius = value;
                CheckChanges();
            }
        }

        public string? directoryPath;
        public string? DirectoryPath
        {
            get
            {
                return directoryPath;
            }
            set
            {
                directoryPath = value;
                CheckChanges();
            }
        }

        public Command Calculate
        {
            get
            {
                return new Command(
                    (obj) =>
                    {

                    }
                );
            }
        }
    }
}