using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal totalTest = 0;
            decimal countKilled = 0;
            string ageOfDeath = "", yearOfDeath = "";
            IProccessMethod proccessMethod = new ProccessMethod();

            do
            {
                do
                {
                    proccessMethod.GetAgeOfDeathText();
                    ageOfDeath = Console.ReadLine();
                    if (ageOfDeath.isNumber()) break;
                    proccessMethod.GetCorrectFormat("Age of Death");
                } while (true);

                do
                {
                    proccessMethod.GetYearOfDeathText();
                    yearOfDeath = Console.ReadLine();
                    if (yearOfDeath.isNumber()) break;
                    proccessMethod.GetCorrectFormat("Year of Death");
                } while (true);

                Console.WriteLine("Process . . .");
                var getPeopleKilled = proccessMethod.CalculateTheKilled(ageOfDeath, yearOfDeath);
                Console.WriteLine("People Killed : {0}", getPeopleKilled);

                totalTest++;
                countKilled += getPeopleKilled >= 1 ? getPeopleKilled : 0;

                Console.WriteLine("Average : {0}", proccessMethod.RoundUp(countKilled, totalTest));
                Console.WriteLine();
                Console.WriteLine("Want to close this app ? Please put (n)/(N)");
                var continueParam = Console.ReadLine();
                if (continueParam == "n" || continueParam == "N") break;
            } while (true);
        }
    }

    #region extension
    public static class ExtensionMethod
    {
        public static bool isNumber(this string str)
        {
            int outVal;
            return int.TryParse(str, out outVal);
        }
    }
    #endregion

    #region proccess
    public class ProccessMethod : IProccessMethod
    {
        public int CalculateTheKilled(string ageOfDeath, string yearOfDeath)
        {
            var tempPeopleKilled2YearsAgo = 0;
            var peopleKilled2YearsAgo = 1;
            var peopleKilledYearsAgo = 0;
            var addedVar = 1;
            var peopleKilled = 0;
            var years = Convert.ToInt32(yearOfDeath) - Convert.ToInt32(ageOfDeath);
            if (years < 1) return -1;
            if (years == 1) return 1;
            else
            {
                for (int i = 0; i < years; i++)
                {
                    if (i > 2) peopleKilled2YearsAgo = tempPeopleKilled2YearsAgo;
                    if (i > 1) tempPeopleKilled2YearsAgo = peopleKilled;
                    peopleKilled = (i > 1 ? peopleKilled2YearsAgo : 0) + peopleKilledYearsAgo + addedVar;
                    peopleKilledYearsAgo = peopleKilled;
                }
            }
            return peopleKilled;
        }

        public decimal RoundUp(decimal amount = 0, decimal length = 0)
        {
            return Math.Round(amount / length, 1);
        }

        public void GetAgeOfDeathText()
        {
            Console.Write("Input Age of Death (0-9) : ");
        }

        public void GetYearOfDeathText()
        {
            Console.Write("Input Year of Death (0-9) : ");
        }

        public void GetCorrectFormat(string param)
        {
            Console.Write("Plese fill {0} with corect format (0-9)", param); Console.WriteLine();
        }
    }

    public interface IProccessMethod
    {
        int CalculateTheKilled(string ageOfDeath, string yearOfDeath);
        decimal RoundUp(decimal amount = 0, decimal length = 0);
        void GetAgeOfDeathText();
        void GetYearOfDeathText();
        void GetCorrectFormat(string param);
    }
    #endregion


}
