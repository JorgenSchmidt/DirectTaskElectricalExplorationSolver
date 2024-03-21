using Core.Configuration;
using Core.Constants;
using Model.FileService;
using Model.MessageService.MessageEntities;

namespace Model.ComponentOperationService
{
    public class ConfigurationOperator
    {
        private static string[] ParameterNames =
        {
            "Кол-во_пикетов",
            "Знач_MN2",
            "Сила_тока",
            "Вмещ_сопр",
            "Шар_сопр",
            "Вмещ_поляр",
            "Шар_поляр",
            "Глуб_залег",
            "Рад_шар"
        };

        /// <summary>
        /// "Забивает" во внутренние переменные программы данные из конфигурационного файла
        /// </summary>
        public static void PutParamFileDataToConfiguration()
        {
            string ConfigFileName =       FileSystemConstants.ConfigurationPathName
                                        + "\\"
                                        + FileNamesConstants.ParametersConfigFileName;

            var ContentSet = ContentGetters.GetContentFromFile(ConfigFileName);
            var Lines = ContentSet.Data.Split('\n');

            int picketCount = 0;
            double halfDistanceBetweenMN = 0;
            double amperageStrength = 0;
            double hostResistance = 0;
            double sphereResistance = 0;
            double hostPolarzability = 0;
            double spherePolarzability = 0;
            double sphereDepth = 0;
            double sphereRadius = 0;


            for (int x = 0; x <= 8; x++)
            {
                var parts = Lines[x].Split('=');
                if (!parts[0].Equals(ParameterNames[x]))
                {
                    MessageObjects.Sender.SendMessage("Имя в строке " + x + " отличается от назначенного ему.");
                    return;
                }
                // Дальше лютейший г**нокод
                if (x == 0) picketCount = Convert.ToInt32(parts[1]); 
                if (x == 1) halfDistanceBetweenMN = Convert.ToDouble(parts[1]); 
                if (x == 2) amperageStrength = Convert.ToDouble(parts[1]);
                if (x == 3) hostResistance = Convert.ToDouble(parts[1]);
                if (x == 4) sphereResistance = Convert.ToDouble(parts[1]);
                if (x == 5) hostPolarzability = Convert.ToDouble(parts[1]);
                if (x == 6) spherePolarzability = Convert.ToDouble(parts[1]);
                if (x == 7) sphereDepth = Convert.ToDouble(parts[1]);
                if (x == 8) sphereRadius = Convert.ToDouble(parts[1]);
            }

            MainParameters.PicketCount = picketCount;
            MainParameters.HalfDistanceBetweenMN = halfDistanceBetweenMN;
            MainParameters.AmperageStrength = amperageStrength;
            MainParameters.HostResistance = hostResistance;
            MainParameters.SphereResistance = sphereResistance;
            MainParameters.HostPolarzability = hostPolarzability;
            MainParameters.SpherePolarzability = spherePolarzability;
            MainParameters.SphereDepth = sphereDepth;
            MainParameters.SphereRadius = sphereRadius;
        }
    }
}