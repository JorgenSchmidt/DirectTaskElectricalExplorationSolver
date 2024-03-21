using Core.Entities.BasicEntities;

namespace Model.FileService
{
    public class ContentGetters
    {
        /// <summary>
        /// Ожидает получить на вход путь до файла с его именем по шаблону "Первая директория\Вторая директория\..\[имя файла]" и расширение файла.
        /// Обратная косая черта в передаваемой строке не ставится.
        /// </summary>
        /// <param name="FilePath"> Относительный путь до файла (относительно исполняемого .exe файла программы). </param>
        /// <returns> Возвращает объект, в случае успеха содержащий контент файла, значение поля IsSuccsess равным true. В обратном случае сообщение об ошибке и значение поля IsSuccsess равным false. </returns>
        public static SimpleData<string> GetContentFromFile(string FilePath)
        {
            SimpleData<string> Result = new SimpleData<string>();
            Result.Data = "";
            Result.ErrorMessage = "";

            // Оставлено на случай изменения логики
            string ActuallyFilePath = FilePath;

            var file = new FileInfo(ActuallyFilePath);
            if (!file.Exists || file.Length == 0)
            {
                Result.ErrorMessage = "Проверьте существует ли файл " + FilePath + " и не является ли его содержимое пустым.";
                Result.IsSucsess = false;
                return Result;
            }

            try
            {
                Result.Data = File.ReadAllText(ActuallyFilePath);
                Result.IsSucsess = true;
            }
            catch (Exception ex)
            {
                Result.ErrorMessage = ex.Message;
                Result.IsSucsess = false;
            }

            return Result;
        }
    }
}