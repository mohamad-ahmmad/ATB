using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ATB.CommandApp.Util
{
    public static class ConsoleUtilites
    {
        public const string Divider = "--------------------------------------------------------------------";

        public static string GetFromConsoleNotNullOrEmpty()
        {
            string? str = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(str!))
            {
                Console.WriteLine("Invalid input, Enter again : ");
                str = Console.ReadLine();
            }

            return str.Trim();
        }

        public static string GetValidEmail()
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex reg = new Regex(pattern);

            string? email = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(email) || !reg.IsMatch(email))
            {
                Console.WriteLine($"Invaild email.");
                email = Console.ReadLine();
            }

            return email;
        }
    }
}
