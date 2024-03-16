using Model.MessageService.MessageEntities;

namespace Model.FileService
{
    public class ContentWriters
    {
        public static void WriteContentToFile(string FilePath, string Content, bool GetMessage)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                {
                    throw new Exception("Папка по полному пути не найдена: \n\n" + FilePath);
                }

                using (StreamWriter stream = new StreamWriter(FilePath))
                {
                    stream.Write(Content);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                if (GetMessage) { MessageObjects.Sender.SendMessage("Ошибка: \n" + ex.Message); }
            }
        }
    }
}