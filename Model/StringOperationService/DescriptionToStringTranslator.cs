using Core.Entities;

namespace Model.StringOperationService
{
    public class DescriptionToStringTranslator
    {
        public static string TranslateWithResistance(Description description) 
        {
            string Answer = "";
            int count = 0;

            foreach (var Profile in description.AnomalyObjects)
            {
                foreach (var Value in Profile.Values)
                {
                    if (Value.Resistance > 0)
                    {
                        Answer += Value.X + "\t" + (-1) * Value.H + "\t" + Value.Resistance + "\n";
                        count += 1;
                    }
                }
            }

            return Answer.Replace(",", ".");
        }

        public static string TranslateWithAddiveResistance(Description description)
        {
            string Answer = "";
            int count = 0;

            foreach (var Profile in description.AnomalyObjects)
            {
                foreach (var Value in Profile.Values)
                {
                    if (Value.Resistance_e > 0)
                    {
                        Answer += Value.X + "\t" + (-1) * Value.H + "\t" + Value.Resistance_e + "\n";
                        count += 1;
                    }
                }
            }

            return Answer.Replace(",", ".");
        }

        public static string TranslateWithPolarzability(Description description)
        {
            string Answer = "";
            int count = 0;

            foreach (var Profile in description.AnomalyObjects)
            {
                foreach (var Value in Profile.Values)
                {
                    if (Value.Polarzability > 0)
                    {
                        Answer += Value.X + "\t" + (-1) * Value.H + "\t" + Value.Polarzability + "\n";
                        count += 1;
                    }
                }
            }

            return Answer.Replace(",", ".");
        }
    }
}