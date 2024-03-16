using Core.Constants;
using DirectTaskElectricalExplorationSolver.AppServise;
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