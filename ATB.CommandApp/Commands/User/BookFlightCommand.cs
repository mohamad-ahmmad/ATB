using ATB.BL.Services.Booking;
using ATB.BL.Services.Flight;
using ATB.CommandApp.Commands.User.Util;
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
    public class BookFlightCommand : ICommand
    {
        private readonly IFlightServices flightServices;
        private readonly IBookingServices bookingServices;

        public BookFlightCommand(IFlightServices flightServices, IBookingServices bookingServices)
        {
            this.flightServices = flightServices;
            this.bookingServices = bookingServices;
        }

        

        public void Execute()
        {

            //Get all flights
            var availableFlights = flightServices.GetFlightsUsingFilter(new FlightFilter(null, null, null, null, null, null, null));
            BookingModel? ToBook = BookingRetrievingUtil.GetBookingFromUserBySpecifingFlightIdAndClassFlightType(availableFlights);

            if(ToBook == null)
                return;
            

            bookingServices.BookAFlight(ToBook.UserId, ToBook.FlightId, ToBook.FlightClass);

            Console.WriteLine(ConsoleUtilites.Divider);
            Console.WriteLine("Booked Successfuly");
        }
    }
}
