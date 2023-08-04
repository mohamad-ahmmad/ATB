using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Repositories.Bookings
{
    public interface IBookingRepository
    {
        public OperationStatusEnum AddBooking(BookingModel booking);
        public List<BookingModel> GetAllBookings();
        public List<BookingModel> GetAllBookings(ulong UserId);
        public OperationStatusEnum UpdateBookingFlightClass(BookingModel from, FlightClassEnum to);

        public OperationStatusEnum DeleteBooking(BookingModel booking);

    }
}
