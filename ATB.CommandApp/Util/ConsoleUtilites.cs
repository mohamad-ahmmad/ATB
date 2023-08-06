using ATB.DA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        public static int? GetNullOrValidPrice()
        {
            string? price = Console.ReadLine();
            
            while (price != null && price != "" && !int.TryParse(price, out int result)) 
            {
                Console.WriteLine("Invalid input.");
                price = Console.ReadLine();
            }
                

            if (price == null || price == "")
                return null;

            return int.Parse(price);
        }

        public static string? GetNullOrNotEmptyString()
        {
            string? str = Console.ReadLine();

            if (str is null || string.IsNullOrEmpty(str.Trim()))
                return null;

            return str.Trim();
        }

        public static DateTime? GetNullOrValidDate()
        {
            string? date = Console.ReadLine();

            while (date is not null && date != "" && !DateTime.TryParse(date.Trim(), out DateTime result))
            {
                Console.WriteLine("Invalid Input.");
                date = Console.ReadLine();
            }

            if(date is null || date.Trim() == string.Empty)
                return null;

            DateTime.TryParse(date.Trim(), out DateTime res);
            return res;
        }

        internal static ulong? GetNullOrValidULong()
        {
            string? id = Console.ReadLine();

            while (id != null && id != "" && !ulong.TryParse(id, out ulong result))
            {
                Console.WriteLine("Invalid input.");
                id = Console.ReadLine();
            }


            if (id == null || id == "")
                return null;

            return ulong.Parse(id);
        }

        public static FlightClassEnum? GetNullOrFlightClass()
        {
            
            var validClasses = new Dictionary<string, FlightClassEnum>();
            validClasses.Add("economy", FlightClassEnum.Economy);
            validClasses.Add("business", FlightClassEnum.Business);
            validClasses.Add("first class", FlightClassEnum.FirstClass);

            string? flightClass = Console.ReadLine();

            while(flightClass != null && flightClass != "" && !validClasses.ContainsKey(flightClass.ToLower().Trim()))
            {
                Console.WriteLine("Invalid input.");
                flightClass = Console.ReadLine();
            }

            if (flightClass is null || string.IsNullOrEmpty(flightClass.Trim()))
                return null;

            return validClasses[flightClass.ToLower().Trim()];

        }
    }
}
