using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Services.Booking
{
    public interface IBookingServices
    {
        public List<FlightModel> GetAllBookings(ulong UserId);
        public bool CancelBooking(BookingModel booking);
        public bool BookAFlight(ulong userId, ulong flightId, FlightClassEnum flightClass);
        public bool ChangeFlightClass(BookingModel booking, FlightClassEnum to );
    }
}
