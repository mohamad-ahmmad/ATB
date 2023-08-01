using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Test.BookingsTests
{
    [TestClass]
    public class BookingModelTests
    {
        [TestMethod]
        public void ToCSV_ConvertsBookingModelToCSVString_Successful()
        {
            // Arrange
            BookingModel bookingModel = new BookingModel(12345, 67890, FlightClassEnum.Business);

            // Act
            string csvString = bookingModel.ToCSV();

            // Assert
            string expectedCSV = "12345,67890,1"; // The expected CSV representation based on the provided FlightClassEnum mapping
            Assert.AreEqual(expectedCSV, csvString);
        }

        [TestMethod]
        public void FromCSV_ConvertsCSVStringToBookingModel_Successful()
        {
            // Arrange
            string csvString = "98765,54321,2"; // CSV representation of BookingModel with FlightClassEnum mapped to '2' (e.g., Economy)

            // Act
            BookingModel bookingModel = BookingModel.FromCSV(csvString);

            // Assert
            Assert.AreEqual((ulong)98765, bookingModel.UserId);
            Assert.AreEqual((ulong)54321u, bookingModel.FlightId);
            Assert.AreEqual(FlightClassEnum.FirstClass, bookingModel.FlightClass);
        }

        [TestMethod]
        public void ToCSV_FromCSV_ReturnsOriginalBookingModel_Successful()
        {
            // Arrange
            BookingModel originalBookingModel = new BookingModel(12345, 67890, FlightClassEnum.Business);

            // Act
            string csvString = originalBookingModel.ToCSV();
            BookingModel recreatedBookingModel = BookingModel.FromCSV(csvString);

            // Assert
            Assert.AreEqual(originalBookingModel.UserId, recreatedBookingModel.UserId);
            Assert.AreEqual(originalBookingModel.FlightId, recreatedBookingModel.FlightId);
            Assert.AreEqual(originalBookingModel.FlightClass, recreatedBookingModel.FlightClass);
        }

        [TestMethod]
        public void FromCSV_InvalidCSVFormat_ThrowsException()
        {
            // Arrange
            string invalidCsvString = "12345,67890"; // CSV format missing FlightClassEnum value

            // Act & Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => BookingModel.FromCSV(invalidCsvString));
        }
    }
}
