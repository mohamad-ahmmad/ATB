using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Test.BookingsTests
{
    [TestClass]
    public class FileBookingRepositoryTests
    {
        private const string _filePath = @"C:\Users\USER-M\source\repos\ATB\ATB.DA\Data\bookings.csv";
        private List<BookingModel> GetMockOfBookingData() 
            => new List<BookingModel>
        {
            new BookingModel(1, 101, FlightClassEnum.Economy),
            new BookingModel(2, 202, FlightClassEnum.Business),
            new BookingModel(3, 303, FlightClassEnum.FirstClass),
            new BookingModel(4, 404, FlightClassEnum.Economy),
            new BookingModel(5, 505, FlightClassEnum.Business)
        };

        //[TestInitialize]
        private void ClearFileContent() => File.WriteAllBytes(_filePath,Array.Empty<byte>());


        
        [TestMethod]
        public void AddBooking_SuccessfulBooking_ReturnsSuccessStatus()
        {
            ClearFileContent();
            // Arrange
            FileBookingRepository bookingRepo = new();
            BookingModel booking = new BookingModel(1, 101, FlightClassEnum.Economy);

            // Act
            OperationStatusEnum status = bookingRepo.AddBooking(booking);

            // Assert
            Assert.AreEqual(OperationStatusEnum.Success, status);

            //Check the content of the file

            string? lineActual = File.ReadAllLines(_filePath).FirstOrDefault();
            string expected = "1,101,0";

            Assert.AreEqual(expected, lineActual);
        }

        [TestMethod]
        public void AddBooking_FailedBooking_ReturnsFailedStatus()
        {
            ClearFileContent();
            // Arrange
            FileBookingRepository bookingRepo = new();
            BookingModel booking = null; // Invalid booking (null)

            // Act
            OperationStatusEnum status = bookingRepo.AddBooking(booking);

            // Assert
            Assert.AreEqual(OperationStatusEnum.Failed, status);
        }


        /*
         Scenario update a book to another FlightClass.
         */
        [TestMethod]
        public void UpdateBookingFlightClass_UpdateTheBookingsSuccessfully()
        {
            //Arrange
            ClearFileContent();
            FileBookingRepository repo = new();

            var books = GetMockOfBookingData();
            books.ForEach(book => repo.AddBooking(book));
            //Act
            OperationStatusEnum act= repo.UpdateBookingFlightClass(books[0], FlightClassEnum.Business);

            //Expected
            OperationStatusEnum expected = OperationStatusEnum.Success;

            Assert.AreEqual(expected, act);
        }

        //[TestCleanup]
        public void DoNothing() { }
    }
}
