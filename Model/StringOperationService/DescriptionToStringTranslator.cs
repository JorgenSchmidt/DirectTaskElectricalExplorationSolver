using Core.Entities;

namespace Model.StringOperationService
{
    public class DescriptionToStringTranslator
    {
        public static string Translate(Description description) 
        {
            string Answer = "";
            int count = 0;

            foreach (var Profile in description.AnomalyObjects)
            {
                foreach (var Value in Profile.Values)
                {
                    Answer += Value.X + "\t" + (-1) * Value.H + "\t" + Value.Value + "\n";
                    count+=1;
                }
            }

            return Answer.Replace(",", ".");
        }
    }
}