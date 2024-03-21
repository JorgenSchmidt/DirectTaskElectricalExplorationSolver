using Core.Constants;
using Model.FileService;
using Model.MessageService.MessageEntities;

namespace Model.ValidateService
{
    public class ConfigFilesChecker
    {
        public static bool CheckParametersFile ()
        {
            string ConfigFileName =       FileSystemConstants.ConfigurationPathName
                                        + "\\"
                                        + FileNamesConstants.ParametersConfigFileName;

            var ContentSet = ContentGetters.GetContentFromFile(ConfigFileName);

            if (!ContentSet.IsSucsess)
            {
                MessageObjects.Sender.SendMessage("Неизвестная ошибка на стадии проверки конфигурационного файла.");
                return false;
            }

            if (String.IsNullOrEmpty(ContentSet.Data))
            {
                MessageObjects.Sender.SendMessage("Содержимое конфигурационного файла оказалось пустым.");
                return false;
            }

            var strings = ContentSet.Data.Split('\n');

            if (strings.Length != 9)
            {
                MessageObjects.Sender.SendMessage("Конфигурационный файл содержал число строк не равным 9.");
                return false;
            }

            foreach (var item in strings) 
            {
                if (!item.Contains('='))
                {
                    MessageObjects.Sender.SendMessage("Одна из строк конфигурационного файла не содержала знак равенства.");
                    return false;
                }
                var parts = item.Split("=");
                if (parts.Length != 2)
                {
                    MessageObjects.Sender.SendMessage("Каждая строка конфигурационного файла должна разбиваться знаком равенства на две части.");
                    return false;
                }
                if (String.IsNullOrEmpty(parts[0]) || String.IsNullOrEmpty(parts[1]))
                {
                    MessageObjects.Sender.SendMessage("Каждая строка конфигурационного файла должна содержать как минимум по одному символу.");
                    return false;
                }
                if (!Double.TryParse(parts[1], out double x))
                {
                    MessageObjects.Sender.SendMessage("Вторая часть каждой строки (значение после знака равенства) должна быть числом.");
                    return false;
                }
            }

            return true;
        }
    }
}