using Core.Entities;
using Core.Entities.GraphicShellEntities;
using DirectTaskElectricalExplorationSolver.AppServise;
using GFDirectTasksSolver.ViewModelService;
using Model.CalculateAnomalyValueService;
using Model.GraphicShell;
using Model.StringOperationService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DirectTaskElectricalExplorationSolver.ViewModels
{
    public class AppWindowViewModel : NotifyPropertyChanged
    {
        #region конфигурация основных графических элементов
        public int canvasWidth = GraphicShellConfiguration.CanvasWidth;
        public int CanvasWidth
        {
            get
            {
                return canvasWidth;
            }
            set
            {
                canvasWidth = value;
                CheckChanges();
            }
        }
        public int canvasHeight = GraphicShellConfiguration.CanvasHeight;
        public int CanvasHeight
        {
            get
            {
                return canvasHeight;
            }
            set
            {
                canvasHeight = value;
                CheckChanges();
            }
        }
        public int frameWidth = GraphicShellConfiguration.CanvasWidth + 2;
        public int FrameWidth
        {
            get
            {
                return frameWidth;
            }
            set
            {
                frameWidth = value;
                CheckChanges();
            }
        }
        public int frameHeight = GraphicShellConfiguration.CanvasHeight + 2;
        public int FrameHeight
        {
            get
            {
                return frameHeight;
            }
            set
            {
                frameHeight = value;
                CheckChanges();
            }
        }
        #endregion

        #region переменные для взаимодействия с графическими элементами приложения
        public List<Line> anomalyModelLines = new List<Line>() ;

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
                async (obj) =>
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
                        if (SphereDepth < 1.5 * SphereRadius)
                        {
                            MessageBox.Show("Значение глубины залегания шара должно превышать его радиус более чем в 1.5 раза.");
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
                            + " dρ-" + (Math.Round(HostResistance - SphereResistance,6)).ToString() + "Ом°м"
                            + " I-" + AmperageStrength.ToString() + "A" 
                            + " R-" + SphereRadius.ToString() + "м"
                            + " h-" + SphereDepth.ToString() + "м";

                        // Проверка папки на существование
                        if (Directory.Exists(DirectoryPath + "\\" + anomalyDescription.AnomalyDescription))
                        {
                            MessageBox.Show("Папка с такими данными уже существует");
                            return;
                        }

                        // Блок начала расчётов
                        double ProfileLength = Math.Round(2 * (PicketCount - 1) * (HalfDistanceBetweenMN), 4);
                        double StartProfilePoint = Math.Round((ProfileLength / 2) * -1, 4);
                        double EndProfilePoint = Math.Round((ProfileLength / 2), 4);
                        double StepByProfile = Math.Round(HalfDistanceBetweenMN * 2, 2);

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

                        // Операции с переменными, ассоциированными с графической составляющей приложения
                        AnomalyModelLines = LineSketcher.DrawLines(anomalyDescription);
                        AnomalyModelSpheres = SphereSketcher.DrawSpheres(ProfileLength, PicketCount, SphereDepth, SphereRadius, anomalyDescription);
                        if (PicketCount < 40)
                        {
                            TextLabels = TextSketcher.WriteLabels(PicketCount, AnomalyModelSpheres);
                        }
                        else
                        {
                            // Обновление списка подписей должно производиться обязательно
                            TextLabels = new List<TextLabel>();
                        }

                        // Создание отдельной папки для результатов
                        string FilePath = DirectoryPath + "\\" + anomalyDescription.AnomalyDescription;
                        Directory.CreateDirectory(FilePath);

                        // Операции с файлом вывода информации (формат .dat)
                        using (StreamWriter writer = new StreamWriter(FilePath + "\\" + anomalyDescription.AnomalyDescription + ".dat", false))
                        {
                            await writer.WriteLineAsync(DescriptionToStringTranslator.Translate(anomalyDescription));
                        }
                    }
                );
            }
        }
        #endregion
    }
}