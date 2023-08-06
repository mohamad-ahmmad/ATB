using ATB.BL.Services.Booking;
using ATB.BL.Services.Flight;
using ATB.CommandApp.Util;
using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories.Bookings;
using ATB.DA.Repositories.Flights;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.Manager
{
    public class FilterSearchCommand : ICommand
    {

        //ASK
        private readonly IFlightServices _fightServices;
        private readonly IBookingServices _bookingServices;
        public FilterSearchCommand(IFlightServices flightServices, IBookingServices bookingServices)
            => (_fightServices , _bookingServices) = (flightServices, bookingServices);

        public FlightFilter GetFilterFromUser(out ulong? userId)
        {
            Console.WriteLine("Note : Keep the field empty if you don't want to include it to the filter.");

            Console.Write("Price : ");
            int? price = ConsoleUtilites.GetNullOrValidPrice();

            Console.Write("Departure country : ");
            string? depCountry = ConsoleUtilites.GetNullOrNotEmptyString();

            Console.Write("Destination Country : ");
            string? desCountry = ConsoleUtilites.GetNullOrNotEmptyString();

            Console.Write("Departure Date(yyyy-MM-dd HH:mm:ss) : ");
            DateTime? date = ConsoleUtilites.GetNullOrValidDate();

            Console.Write("Departure Airport : ");
            string? depAirport = ConsoleUtilites.GetNullOrNotEmptyString();

            Console.Write("Arrival Airport : ");
            string? arrivalAirport = ConsoleUtilites.GetNullOrNotEmptyString();

            Console.Write("Passenger Id : ");
            userId = ConsoleUtilites.GetNullOrValidULong();

            Console.Write("Flight Class (Economy/Business/First class) : ");
            FlightClassEnum? flightClass = ConsoleUtilites.GetNullOrFlightClass();

            return new FlightFilter
            (
                price,
                depCountry,
                desCountry,
                date,
                depAirport,
                arrivalAirport,
                flightClass
            );
        }

        public void Execute()
        {
            FlightFilter filter = GetFilterFromUser(out ulong? userId);

            Console.WriteLine(ConsoleUtilites.Divider);

            List<FlightModel> userFlights = new();

            if(userId is not null)
                userFlights=_bookingServices.GetAllBookings((ulong)userId);

            List<FlightSearchResultModel> results = new();

            if (userFlights.Count == 0)
                results = _fightServices.GetFlightsUsingFilter(filter);
            else
                results = _fightServices.GetFlightsUsingFilter(userFlights, filter);

            Console.WriteLine($"There's {results.Count} " + (results.Count == 1 ? "flight" : "flights") + " : ");

            results.ForEach(flight =>
            {
                Console.WriteLine(flight.ToString());
            });
            if(results.Count == 0)
            Console.WriteLine(ConsoleUtilites.Divider);
        }
    }
}
