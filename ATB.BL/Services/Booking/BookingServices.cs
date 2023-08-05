using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories.Bookings;
using ATB.DA.Repositories.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Services.Booking
{
    public class BookingServices : IBookingServices
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IBookingRepository _bookingRepository;
        public BookingServices(IFlightRepository flightRepo, IBookingRepository bookingRepo)
            => (_flightRepository, _bookingRepository) = (flightRepo, bookingRepo);


        /// <summary>
        /// Book a flight.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="flightId"></param>
        /// <param name="flightClass"></param>
        /// <returns>true success, false failed</returns>
        public bool BookAFlight(ulong userId ,ulong flightId, FlightClassEnum flightClass)
            => _bookingRepository.AddBooking(new BookingModel(userId, flightId, flightClass)) == OperationStatusEnum.Success;
        

        /// <summary>
        /// Cancel booking.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns>true if the booking successfully removed, false then the booking doesn't exists.</returns>
        public bool CancelBooking(BookingModel booking)
            => _bookingRepository.DeleteBooking(booking) == OperationStatusEnum.Success;

        public bool ChangeFlightClass(BookingModel booking, FlightClassEnum to)
        => _bookingRepository.UpdateBookingFlightClass(booking, to) == OperationStatusEnum.Success;

        /// <summary>
        /// Retrieve all flights that the user with {UserId} booked.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>List of flights, empty if the user haven't booked a flight yet.</returns>
        public List<FlightModel> GetAllBookings(ulong UserId)
            => _bookingRepository.GetAllBookings(UserId)
                .Join(_flightRepository.GetAllFlights(),
                (book)=>book.FlightId,
                (flight)=> flight.FlightId,
                (booking , flight) =>
                new FlightModel(
                
                    flightId : flight.FlightId,
                    depCountry : flight.DepCountry,
                    arrivalCountry: flight.ArrivalCountry,
                    depDate: flight.DepDate,
                    depAirport : flight.DepAirport,
                    arrivalAirport: flight.ArrivalAirport,
                    flightClasses : flight.FlightClasses.Where((flightClass)=> flightClass.FlightClass == booking.FlightClass).ToList(),
                    dateFormat: flight.DateFormat
                )
                ).ToList();
    }


}
