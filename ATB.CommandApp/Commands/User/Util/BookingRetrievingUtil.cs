using ATB.CommandApp.Util;
using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.User.Util
{
    public class BookingRetrievingUtil
    {
        public static Dictionary<string, FlightClassEnum> GetAvailableClassesForFlight(List<FlightSearchResultModel> flight)
        {
            //To check if the class exist.
            var availableClasses = flight.ToDictionary((flight) => flight.FlightClassModel.FlightClass.ToString().ToLower(),
                                                                (flight) => flight.FlightClassModel.FlightClass);

            availableClasses.Add("first class", FlightClassEnum.FirstClass);
            return availableClasses;
        }

        public static BookingModel? GetBookingFromUserBySpecifingFlightIdAndClassFlightType(List<FlightSearchResultModel> flightsAvailableToUser)
        {
            Console.WriteLine(ConsoleUtilites.Divider);
            Console.WriteLine("Insert the flight id : ");
            ulong? flightId = ConsoleUtilites.GetNullOrValidULong();

            if (flightId == null)
            {
                Console.WriteLine("There's no flight with the provided id.");
                return null;
            }

            //Get all flights
            var availableFlights = flightsAvailableToUser;
            List<FlightSearchResultModel> flightToBook = availableFlights.Where(flight => flight.FlightId == flightId).ToList();
            var availableClasses = GetAvailableClassesForFlight(flightToBook);

            if (flightToBook.Count == 0)
            {
                Console.WriteLine("There's no flight with the provided id.");
                return null;
            }

            Console.WriteLine("Insert the class : ");
            string? flightClass = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(flightClass))
            {
                Console.WriteLine("There's no flight with the provided class.");
                return null;
            }
            flightClass = flightClass.Trim().ToLower();


            if (!availableClasses.ContainsKey(flightClass))
            {
                Console.WriteLine("There's no flight with the provided class.");
                return null;
            }
            return new BookingModel((ulong)AppState.UserId!, (ulong)flightId, availableClasses[flightClass]);
        }
    }
}
