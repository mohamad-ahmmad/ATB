using ATB.BL.Services.Booking;
using ATB.CommandApp.Util;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.User
{
    public class ViewAllBookingsCommand : ICommand
    {
        private IBookingServices _bookingServices;

        public ViewAllBookingsCommand(IBookingServices bookingServices)
            => this._bookingServices = bookingServices;
        

        public void Execute()
        {
            List<FlightModel> bookedFlights = _bookingServices.GetAllBookings((ulong)AppState.UserId!);
            Console.WriteLine(ConsoleUtilites.Divider);

            Console.WriteLine($"You have {bookedFlights.Count} " + (bookedFlights.Count == 1? "flight" : "flights") + " :");

            bookedFlights.ForEach(flight => {
                Console.WriteLine(ConsoleUtilites.Divider);
                Console.Write(flight);
                Console.WriteLine(ConsoleUtilites.Divider);
            });

            Console.WriteLine(ConsoleUtilites.Divider);

        }
    }
}
