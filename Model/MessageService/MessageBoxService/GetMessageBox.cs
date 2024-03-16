using System.Windows;

namespace Model.MessageService.MessageBoxService
{
    public class GetMessageBox
    {
        public static void Show(string Message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(Message);
            });
        }
    }
}