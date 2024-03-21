using Core.Constants;
using DirectTaskElectricalExplorationSolver.AppServise;
using Model.ComponentOperationService;
using Model.FileService;
using Model.ValidateService;
using System.IO;
using System.Windows;

namespace DirectTaskElectricalExplorationSolver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (!Directory.Exists(FileSystemConstants.OutputPathName))
            {
                Directory.CreateDirectory(FileSystemConstants.OutputPathName);
                MessageBox.Show("Папка Output пересоздана.");
            }

            if (!Directory.Exists(FileSystemConstants.ConfigurationPathName))
            {
                // Если папка с конфигурацией не была создана - пересоздаём её, уведомляя об этом пользователя
                Directory.CreateDirectory(FileSystemConstants.ConfigurationPathName);
                MessageBox.Show("Папка Configs пересоздана.");
            }

            // Конкатенируем к папке с конфигурацией имя файла
            string ConfigFileName =       FileSystemConstants.ConfigurationPathName
                                        + "\\"
                                        + FileNamesConstants.ParametersConfigFileName;

            // Проверяем существует ли файл с конфигурацией
            if (!File.Exists(ConfigFileName))
            {
                MessageBox.Show("Файл с конфигурационными данными был пересоздан ввиду его отсутствия.");
                ContentWriters.WriteContentToFile(ConfigFileName, ContentConstants.BasicConfigContent, true);
            }

            // Проверяем файл на соответствие заранее определённой структуре
            if (!ConfigFilesChecker.CheckParametersFile())
            {
                MessageBox.Show("Конфигурационный файл имел неправильный формат и будет пересоздан.");
                ContentWriters.WriteContentToFile(ConfigFileName, ContentConstants.BasicConfigContent, true);
                ConfigurationOperator.PutParamFileDataToConfiguration();
            }
            else
            {
                ConfigurationOperator.PutParamFileDataToConfiguration();
            }

            WindowsObjects.AppWindow = new();
            if (WindowsObjects.AppWindow.ShowDialog() == true)
            {
                WindowsObjects.AppWindow.Show();
            }
            else
            {
                WindowsObjects.AppWindow = null;
                Shutdown();
                return;
            }

        }
    }
}