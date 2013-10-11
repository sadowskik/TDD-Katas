using System.Globalization;

namespace TDD_Katas_project.FizzBuzzKata
{
    public class FizzBuzzGame
    {
        internal string CheckNuber(int p)
        {
            string result = null;
            
            if (p%3 == 0)
                result += "Fizz";

            if (p%5 == 0)
                result += "Buzz";

            return result ?? p.ToString();
        }
    }
}
