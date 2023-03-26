using Core.Entities;
using Core.Entities.GraphicShellEntities;
using DirectTaskElectricalExplorationSolver.AppServise;
using GFDirectTasksSolver.ViewModelService;
using Model.CalculateAnomalyValueService;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace DirectTaskElectricalExplorationSolver.ViewModels
{
    public class AppWindowViewModel : NotifyPropertyChanged
    {
        #region переменные для взаимодействия с графическими элементами приложения
        public List<Line> anomalyModelLines = new List<Line>();

        public List<Line> AnomalyModelLines
        {
            get
            {
                return anomalyModelLines;
            }
            set
            {
                anomalyModelLines = value;
                CheckChanges();
            }
        }

        public List<Sphere> anomalyModelSpheres = new List<Sphere>();
        public List<Sphere> AnomalyModelSpheres
        {
            get
            {
                for (int i = 0; i < anomalyModelSpheres.Count; i++)
                {
                    anomalyModelSpheres[i].SphereMargin = ToThickness.Convert(anomalyModelSpheres[i].X, anomalyModelSpheres[i].Y);
                }
                return anomalyModelSpheres;
            }
            set
            {
                anomalyModelSpheres = value;
                CheckChanges();
            }
        }

        public List<TextLabel> textLabels = new List<TextLabel>();
        public List<TextLabel> TextLabels
        {
            get
            {
                for (int i = 0; i < textLabels.Count; i++)
                {
                    textLabels[i].TextMargin = ToThickness.Convert(textLabels[i].X, textLabels[i].Y);
                }
                return textLabels;
            }
            set
            {
                textLabels = value;
                CheckChanges();
            }
        }
        #endregion

        #region переменные для входных данных (моделируемая среда)
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
        #endregion

        #region переменные для входных данных (файловая система компьютера)
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
        #endregion

        #region регион с описанием алгоритма расчёта и загрузки в элемент canvas данных для отображения как графических элементов
        public Command Calculate
        {
            get
            {
                return new Command(
                    (obj) =>
                    {
                        // Блок обработки исключительных случаев
                        if (PicketCount < 4 || PicketCount % 2 == 1)
                        {
                            MessageBox.Show("Количество пикетов не может быть меньше, чем \"4\" или являться нечётным числом.");
                            return;
                        }
                        if (HalfDistanceBetweenMN <= 0)
                        {
                            MessageBox.Show("Значение MN/2 не может быть меньше или равно \"0\".");
                            return;
                        }
                        if (HostResistance <= 0 )
                        {
                            MessageBox.Show("Значение удельного сопротивления вмещающих пород не может быть меньше или равно \"0\".");
                            return;
                        }
                        if (SphereResistance <= 0)
                        {
                            MessageBox.Show("Значение удельного сопротивления шара не может быть меньше или равно \"0\".");
                            return;
                        }
                        if (HostResistance < 10 * SphereResistance)
                        {
                            MessageBox.Show("Значение удельного сопротивления вмещающих пород должно превышать сопротивление шара более чем в 10 раз.");
                            return;
                        }
                        if (AmperageStrength <= 0)
                        {
                            MessageBox.Show("Значение силы тока не может быть меньше или равно \"0\".");
                            return;
                        }
                        if (SphereDepth <= 0)
                        {
                            MessageBox.Show("Значение глубины залегания шара не может быть меньше или равно \"0\".");
                            return;
                        }
                        if (SphereRadius <= 0)
                        {
                            MessageBox.Show("Значение радиуса шара не может быть меньше или равно \"0\".");
                            return;
                        }
                        if (SphereRadius >= SphereDepth)
                        {
                            MessageBox.Show("Значение радиуса шара не может быть больше или равно значению глубины его залегания.");
                            return;
                        }
                        if (!Directory.Exists(DirectoryPath))
                        {
                            MessageBox.Show("Введите путь до существующей директории.");
                            return;
                        }

                        // Блок инициализации переменной с описанием и значениями аномалии электрического поля
                        Description anomalyDescription = new Description();
                        anomalyDescription.AnomalyDescription =
                              "ВЭЗ ПКК-" + PicketCount.ToString()
                            + " MN-" + (HalfDistanceBetweenMN * 2).ToString() + "м"
                            + " dρ-" + (HostResistance - SphereResistance).ToString() + "Ом°м"
                            + " I-" + AmperageStrength.ToString() + "A" 
                            + " R-" + SphereRadius.ToString() + "м"
                            + " h-" + SphereDepth.ToString() + "м";

                        // Блок начала расчётов
                        double ProfileLength = 2 * (PicketCount - 1) * (HalfDistanceBetweenMN);
                        double StartProfilePoint = (ProfileLength / 2) * -1;
                        double EndProfilePoint = (ProfileLength / 2);
                        double StepByProfile = HalfDistanceBetweenMN * 2;

                        // Блок присваивания полю AnomalyObjects объекта с именем anomalyDescription значения из метода
                        anomalyDescription.AnomalyObjects = AnomalyDescriptionGetter.GetAnomalyDescription(
                                                                                                            // Конфигурация профиля наблюдений
                                                                                                            PicketCount, 
                                                                                                            StartProfilePoint, 
                                                                                                            EndProfilePoint, 
                                                                                                            StepByProfile, 
                                                                                                            // Геометрия моделируемой среды
                                                                                                            SphereDepth, 
                                                                                                            SphereRadius, 
                                                                                                            // Геофизика моделируемой среды
                                                                                                            HostResistance, 
                                                                                                            SphereResistance, 
                                                                                                            AmperageStrength
                                                                                                        ).AnomalyObjects;


                        TextLabels = new List<TextLabel>() { 
                            new TextLabel() { Text = ProfileLength.ToString(), X = 60, Y = 80 },
                            new TextLabel() { Text = StartProfilePoint.ToString(), X = 60, Y = 100 },
                            new TextLabel() { Text = EndProfilePoint.ToString(), X = 60, Y = 120 },
                            new TextLabel() { Text = anomalyDescription.AnomalyDescription, X = 30, Y = 60 },
                        };


                    }
                );
            }
        }
        #endregion
    }
}