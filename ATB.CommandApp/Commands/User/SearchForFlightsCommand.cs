using ATB.BL.Services.Flight;
using ATB.CommandApp.Util;
using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.User
{
    internal class SearchForFlightsCommand : ICommand
    {
        private IFlightServices flightServices;

        public SearchForFlightsCommand(IFlightServices flightServices)
        {
            this.flightServices = flightServices;
        }

        private FlightFilter GetFilterFromUser()
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
            Console.WriteLine(ConsoleUtilites.Divider);

            FlightFilter filter= GetFilterFromUser();
            List<FlightSearchResultModel> flights = flightServices.GetFlightsUsingFilter(filter);

            flights.ForEach( f => {
                Console.WriteLine(ConsoleUtilites.Divider);
                Console.WriteLine(f.ToString());
                Console.WriteLine(ConsoleUtilites.Divider);
            }
            );

        }
    }
}
