using Core.Constants;
using Core.Entities;
using Core.Entities.GraphicShellEntities;
using DirectTaskElectricalExplorationSolver.AppServise;
using GFDirectTasksSolver.ViewModelService;
using Model.CalculateAnomalyValueService;
using Model.FileService;
using Model.GraphicShell;
using Model.StringOperationService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

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
        public int picketCount = 12;
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

        public double halfDistanceBetweenMN = 5;
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

        public double hostResistance = 1;
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

        public double sphereResistance = 0.1;
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

        public double hostPolarzability = 0.02;
        public double HostPolarzability
        {
            get
            {
                return hostPolarzability;
            }
            set
            {
                hostPolarzability = value;
                CheckChanges();
            }
        }

        public double spherePolarzability = 0.2;
        public double SpherePolarzability
        {
            get
            {
                return spherePolarzability;
            }
            set
            {
                spherePolarzability = value;
                CheckChanges();
            }
        }

        public double amperageStrenght = 1;
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

        public double sphereDepth = 10;
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

        public double sphereRadius = 4; 
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

        public bool calculateMediavalPoint = false;

        public bool CalculateMediavalPoint
        {
            get
            {
                return calculateMediavalPoint;
            }
            set
            {
                calculateMediavalPoint = value;
                CheckChanges();
            }
        }
        #endregion

        #region переменные для входных данных (файловая система компьютера)
        public string? directoryName;
        public string? DirectoryName
        {
            get
            {
                return directoryName;
            }
            set
            {
                directoryName = value;
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
                        if (!Directory.Exists(FileSystemConstants.OutputPathName))
                        {
                            Directory.CreateDirectory(FileSystemConstants.OutputPathName);
                        }
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
                            MessageBox.Show("Значение удельного сопротивления вмещающих пород\n должно превышать сопротивление шара более чем в 10 раз.");
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
                        if (    !(HostPolarzability < SpherePolarzability
                                && (HostPolarzability > 0 && HostPolarzability < 1)
                                && (SpherePolarzability > 0 && SpherePolarzability < 1)) )
                        {
                            if (HostPolarzability > 0 && SpherePolarzability > 0)
                            {
                                var Message = "Значение поляризуемости для шара должно быть строго больше чем значение поляризуемости для вмещающей среды.\n"
                                    + "Так же значение поляризуемости должно быть в пределах от 0 до 1.";
                                MessageBox.Show(Message);
                                return;
                            }
                        }
                        if (   DirectoryName.Contains('/') 
                            || DirectoryName.Contains('\\')
                            || DirectoryName.Contains(':')
                            || DirectoryName.Contains('*')
                            || DirectoryName.Contains('?')
                            || DirectoryName.Contains('\"')
                            || DirectoryName.Contains('<')
                            || DirectoryName.Contains('>')
                            || DirectoryName.Contains('|'))
                        {
                            MessageBox.Show("Windows запрещает использовать в имени директории или файла символы:\n \\ \n / \n : \n * \n ? \n \" \n < \n > \n |");
                            return;
                        }

                        // Проверка папки на существование
                        var CurrentPath = FileSystemConstants.OutputPathName + "\\" + DirectoryName;
                        if (Directory.Exists(CurrentPath))
                        {
                            MessageBox.Show("Папка с таким названием уже существует, результаты измерения будут записаны именно в неё.");
                        }
                        else
                        {
                            Directory.CreateDirectory(CurrentPath);
                        }

                        // Блок инициализации основной локальной переменной данных
                        Description anomalyDescription = new Description();

                        // Блок начала расчётов
                        double ProfileLength = Math.Round(2 * (PicketCount - 1) * (HalfDistanceBetweenMN), 4);
                        double StartProfilePoint = Math.Round((ProfileLength / 2) * -1, 4);
                        double EndProfilePoint = Math.Round((ProfileLength / 2), 4);
                        double StepByProfile = Math.Round(HalfDistanceBetweenMN * 2, 2);

                        // Блок присваивания полю AnomalyObjects объекта с именем anomalyDescription значения из метода
                        anomalyDescription = AnomalyDescriptionGetter.GetMainAnomalyDescription(
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
                                                                                                            HostPolarzability,
                                                                                                            SphereResistance, 
                                                                                                            SpherePolarzability,
                                                                                                            AmperageStrength,
                                                                                                            // Конфигурация вычислений
                                                                                                            CalculateMediavalPoint
                                                                                                        );

                        // Операции с переменными, ассоциированными с графической составляющей приложения
                        AnomalyModelLines = LineSketcher.DrawLines(anomalyDescription);
                        AnomalyModelSpheres = SphereSketcher.DrawSpheres(ProfileLength, PicketCount, SphereDepth, SphereRadius, anomalyDescription);

                        if (PicketCount < 40) // Обновление списка подписей должно производиться обязательно
                        {
                            TextLabels = TextSketcher.WriteLabels(PicketCount, AnomalyModelSpheres);
                        }
                        else
                        {
                            TextLabels = new List<TextLabel>();
                        }

                        // Создание краткого текстового описания входных параметров моделирования, используется для задания имени файлу
                        string MedPoint = "";

                        if (anomalyDescription.MediavalPointWasCalculate)
                        {
                            MedPoint = " СТ-НР";
                        }
                        else
                        {
                            MedPoint = " СТ-Р";
                        }

                        string Polar = ""; 

                        if (anomalyDescription.AddiveNotNull)
                        {
                            Polar = " ПЛ-Р";
                        }
                        else
                        {
                            Polar = " ПЛ-НР";
                        }
                        
                        anomalyDescription.AnomalyDescription =
                              "ВЭЗ ПКК-" + PicketCount.ToString()
                            + MedPoint 
                            + Polar
                            + " MN-" + (HalfDistanceBetweenMN * 2).ToString() + "м"
                            + " dρ-" + (Math.Round(HostResistance - SphereResistance, 3)).ToString() + "Ом°м"
                            + " dη-" + (Math.Round(SpherePolarzability - HostPolarzability, 3) * 100).ToString() + "%"
                            + " I-" + AmperageStrength.ToString() + "A"
                            + " R-" + SphereRadius.ToString() + "м"
                            + " h-" + SphereDepth.ToString() + "м";

                        // Генерация контента для файлов (по каждому моделируемому параметру) 
                        // на основе полученных в ходе моделирования данных и сохранение их в соответсвующие файлы

                        var ResistanceData = DescriptionToStringTranslator.TranslateWithResistance(anomalyDescription);

                        ContentWriters.WriteContentToFile(CurrentPath + "\\" + "СОПР " + anomalyDescription.AnomalyDescription + ".dat", ResistanceData, true);
                        
                        if (anomalyDescription.AddiveNotNull)
                        {
                            var AddiveResistanceData = DescriptionToStringTranslator.TranslateWithAddiveResistance(anomalyDescription);
                            var EtaData = DescriptionToStringTranslator.TranslateWithPolarzability(anomalyDescription);

                            ContentWriters.WriteContentToFile(CurrentPath + "\\" + "ДСОПР " + anomalyDescription.AnomalyDescription + ".dat", AddiveResistanceData, true);
                            ContentWriters.WriteContentToFile(CurrentPath + "\\" + "ПОЛЯР " + anomalyDescription.AnomalyDescription + ".dat", EtaData, true);
                        }

                    }
                );
            }
        }

        public Command Help
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        string content =    "Данная программа используется для моделирования результатов электротомографии для шара.\n\n"
                                          + "Для начала работы нужно ввести основные входные параметры, а так же название директории, \n"
                                          + "при этом поле для названия директории можно оставить пустым (файлы с данными будут сохранены непосредственно в папку Output корневого каталога программы). \n"
                                          + "Если поле ввода директории сохранения будет заполнено, то файлы с данными сохранятся по пути Output\\<Название директории>. \n\n"
                                          
                                          + "Требования для входных параметров: \n"
                                          + "1. Количество пикетов - чётное число\n"
                                          + "2. Значение удельного сопротивления вмещающих пород должно превышать сопротивление шара более чем в 10 раз.\n"
                                          + "3. Значение поляризуемости для шара должно быть в 10 раз больше значения поляризуемости вмещающих пород и находиться в пределах от 0 до 1.\n\n"
                                          
                                          + "Значения поляризуемости можно оставить равными нулю, тогда будет расчитаны значения только для КС.\n"
                                          + "Также поддерживается возможность исключить из расчётов значения при которых Х=0 (середина профиля наблюдений)\n";

                        MessageBox.Show(content);
                    }    
                );
            }
        }
        #endregion
    }
}