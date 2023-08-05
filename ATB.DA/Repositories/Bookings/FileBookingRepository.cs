using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Repositories.Bookings
{
    public class FileBookingRepository : IBookingRepository
    {
        private const string _filePath = @"C:\Users\USER-M\source\repos\ATB\ATB.DA\Data\bookings.csv";
        private List<BookingModel> _books;

        public FileBookingRepository()
        {
            _books = GetAllBookingFromFile();
        }

        /// <summary>
        /// Read all the books stored in the file.
        /// </summary>
        /// <returns>List of BookingModel</returns>
        private static List<BookingModel> GetAllBookingFromFile()
        {
            string[] bookingsFileLines = File.ReadAllLines(_filePath);
            if (bookingsFileLines.Length == 0)
                return new List<BookingModel>();
            
            return bookingsFileLines.Select(line =>
            {
                string[] bookingInfo = line.Split(",");
                return new BookingModel
                    (
                        ulong.Parse(bookingInfo[0]),
                        ulong.Parse(bookingInfo[1]),
                        (FlightClassEnum)int.Parse(bookingInfo[2])
                    );

            }).ToList();
        }

        /// <summary>
        /// Add book to the storage.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns>OperationStatusEnum</returns>
        public OperationStatusEnum AddBooking(BookingModel booking)
        {
            try
            {
                File.AppendAllText(_filePath, booking.ToCSV()+"\r\n");
                _books.Add(booking);
                return OperationStatusEnum.Success;
            }catch (Exception ex)
            {
                Console.WriteLine("Error : "+ex.ToString());
                return OperationStatusEnum.Failed;
            }
            
        }

        /// <summary>
        /// Return the reference of the given booking inside the books.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns>Booking object if successfully found, else null</returns>
        public BookingModel? Search(BookingModel booking)
            => _books.Find((book)=> book.EqualsTo(booking));

        /// <summary>
        /// Gives csv string format for the current bookings
        /// </summary>
        /// <returns></returns>
        private string AllBookingsCSVFormat()
        => _books.Aggregate(
                                new StringBuilder(),
                                (builder, book) => builder.Append(book.ToCSV() + "\r\n")
                           ).ToString();


        /// <summary>
        /// Delete a booking.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns>true if item successfully removed, false then the item doesn't exists.</returns>
        public OperationStatusEnum DeleteBooking(BookingModel booking)
        {
            OperationStatusEnum res =
               _books.Remove(Search(booking)!) ? OperationStatusEnum.Success : OperationStatusEnum.Failed;

            if(res == OperationStatusEnum.Success)
                File.WriteAllText(_filePath, AllBookingsCSVFormat());
            
            return res;
        }

        /// <summary>
        /// Get all the books in the storage
        /// </summary>
        /// <returns>List of BookingModel</returns>
        public List<BookingModel> GetAllBookings() => _books;
        

        /// <summary>
        /// Return all the bookings for a user with UserId ID.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>List of BookingModel</returns>
        public List<BookingModel> GetAllBookings(ulong UserId)
            => _books.Where(book => book.UserId == UserId).ToList();

        /// <summary>
        /// Update the flight class of a booking.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>OperationStatusEnum</returns>
        public OperationStatusEnum UpdateBookingFlightClass(BookingModel from, FlightClassEnum to)
        {
            BookingModel? toChange = Search(from);
            if (toChange is null)
                return OperationStatusEnum.Failed;

            FlightClassEnum olderValue =  from.FlightClass;
            toChange.FlightClass = to;

            //build csv format for the bookings
            string booksCSVFormat = AllBookingsCSVFormat();

            try
                {
                File.WriteAllText(_filePath, booksCSVFormat);
                return OperationStatusEnum.Success;
                }
            catch(Exception ex)
                {
                    Console.WriteLine("Error : " + ex.ToString());
                    from.FlightClass=olderValue;
                    return OperationStatusEnum.Failed;
                }
        }
    }
}
