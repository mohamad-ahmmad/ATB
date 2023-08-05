using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Test.FlightsTests
{
    [TestClass]
    public class FlightModelTests
    {
        private List<FlightModel> GetMockFlightData() => FlightMockData.GetFlightsMockData();
       
        [TestMethod]
        public void ToCSV_ShouldReturnValidCSVString()
        {
            // Arrange
            ulong flightId = 12345;
            string depCountry = "USA";
            string arrivalCountry = "UK";
            DateTime depDate = new DateTime(2023, 7, 31, 12, 30, 0);
            string depAirport = "JFK";
            string arrivalAirport = "LHR";
            List<FlightClassModel> flightClasses = new()
        {
            new FlightClassModel(FlightClassEnum.Economy, 500, 200),
            new FlightClassModel(FlightClassEnum.Business, 1500, 50),
            new FlightClassModel(FlightClassEnum.FirstClass, 3000, 20)
        };
            string dateFormat = "dd/MM/yyyy HH:mm:ss"; // Custom date format

            FlightModel flightModel = new(flightId, depCountry, arrivalCountry, depDate, depAirport, arrivalAirport, flightClasses, dateFormat);

            // Act
            string result = flightModel.ToCSV();

            // Assert
            string expectedCSV = "12345,USA,UK,31/07/2023 12:30:00,JFK,LHR,dd/MM/yyyy HH:mm:ss,3\r\n" +
                                "0,500,200\r\n" +
                                "1,1500,50\r\n" +
                                "2,3000,20\r\n";
            Assert.AreEqual(expectedCSV, result);
        }

        [TestMethod]
        public void ToCSV_WhenDateFormatIsNull_ShouldUseDefaultDateFormat()
        {
            //Arrange

            //The second element in FlightData have a null DateFormat
            FlightModel flightModel = GetMockFlightData()[1];

            // Act
            string result = flightModel.ToCSV();

            // Assert
            string expectedCSV = "1,UK,France,2023-08-15 08:00:00,LHR,CDG,default,3\r\n" +
                "0,400,250\r\n" +
                "1,1200,40\r\n" +
                "2,2500,15\r\n";
            Assert.AreEqual(expectedCSV, result);
        }

        [TestMethod]
        public void FromCSV_ShouldParseCSVStringCorrectly()
        {
            // Arrange
            var mockData = GetMockFlightData();

            // Actual
            FlightModel expectedFlightModel = mockData[0];

            // Expected
            string csv = mockData[0].ToCSV();
            FlightModel resultFlightModel = FlightModel.FromCSV(csv);

            // Assert
            Assert.IsTrue(expectedFlightModel.EqualsTo(resultFlightModel));

        }

        [TestMethod]
        public void FromCSV_DefaultDateFormat_ShouldParseCorrectly()
        {
            // Arrange
            var mockData = GetMockFlightData();

            //Act
            FlightModel expectedFlightModel = mockData[1];

            //Second element contains default DateFormat
            //Expected
            string csv = mockData[1].ToCSV();
            FlightModel resultFlightModel = FlightModel.FromCSV(csv);

            // Assert
            Assert.IsTrue(expectedFlightModel.EqualsTo(resultFlightModel));

        }

        [TestMethod]
        public void FlightModel_ValidData_ShouldBeValid()
        {
            // Arrange
            var flightModel = new FlightModel(1u, "USA", "UK", DateTime.Now.AddDays(2), "JFK","LHR",null!,null);

            // Act
            var context = new ValidationContext(flightModel, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(flightModel, context, validationResults, true);

            foreach(var validationResult in validationResults)
                Console.WriteLine(validationResult.ErrorMessage);
            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void FlightModel_InvalidData_ShouldNotBeValid()
        {
            // Arrange
            var flightModel = new FlightModel(3u, "USA123", "", DateTime.Now.AddDays(6), "JFK$#", "LHR", null!, null);

            // Act
            var context = new ValidationContext(flightModel);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(flightModel, context, validationResults, true);

            foreach(var item in validationResults)
                Console.WriteLine(item.ErrorMessage);
            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(3, validationResults.Count); // 3 properties are invalid
        }


    }
}
