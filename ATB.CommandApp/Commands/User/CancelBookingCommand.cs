using ATB.BL.Services.Booking;
using ATB.CommandApp.Commands.User.Util;
using ATB.CommandApp.Util;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.User
{
    public class CancelBookingCommand : ICommand
    {
        private IBookingServices bookingServices;

        public CancelBookingCommand(IBookingServices bookingServices)
        {
            this.bookingServices = bookingServices;
        }

        public void Execute()
        {
            var availableFlightsToCancel = bookingServices.GetAllBookings((ulong)AppState.UserId!);

            //This method should be in the utilites of the models.
            List<FlightSearchResultModel> converted = availableFlightsToCancel
                .Select(flight =>
                new FlightSearchResultModel
                (
                    flight.FlightId,
                    flight.DepCountry,
                    flight.ArrivalAirport,
                    flight.DepDate,
                    flight.DepAirport,
                    flight.ArrivalAirport,
                    new FlightClassModel(flight.FlightClasses[0].FlightClass, flight.FlightClasses[0].Price, flight.FlightClasses[0].Capacity)
                )).ToList();

            BookingModel? toCancel = BookingRetrievingUtil.GetBookingFromUserBySpecifingFlightIdAndClassFlightType(converted);

            if(toCancel == null)
                return;

            bookingServices.CancelBooking(toCancel);

            Console.WriteLine(ConsoleUtilites.Divider);
        }
    }
}
