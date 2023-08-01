using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Test.FlightsTests
{
    [TestClass]
    public class FlightModelTests
    {
        private List<FlightModel> GetMockFlightData()
        {
            List<FlightModel> flights = new List<FlightModel>
    {
        new FlightModel(
            FlightId: 1,
            DepCountry: "USA",
            ArrivalCountry: "UK",
            DepDate: new DateTime(2023, 7, 31, 12, 30, 0),
            DepAirport: "JFK",
            ArrivalAirport: "LHR",
            FlightClasses: new List<FlightClassModel>
            {
                new FlightClassModel(FlightClassEnum.Economy, 500, 200),
                new FlightClassModel(FlightClassEnum.Business, 1500, 50),
                new FlightClassModel(FlightClassEnum.FirstClass, 3000, 20)
            },
            DateFormat: "dd/MM/yyyy HH:mm:ss"
        ),
        new FlightModel(
            FlightId: 2,
            DepCountry: "UK",
            ArrivalCountry: "France",
            DepDate: new DateTime(2023, 8, 15, 8, 0, 0),
            DepAirport: "LHR",
            ArrivalAirport: "CDG",
            FlightClasses: new List<FlightClassModel>
            {
                new FlightClassModel(FlightClassEnum.Economy, 400, 250),
                new FlightClassModel(FlightClassEnum.Business, 1200, 40),
                new FlightClassModel(FlightClassEnum.FirstClass, 2500, 15)
            },
            null
        ),
        new FlightModel(
            FlightId: 3,
            DepCountry: "Germany",
            ArrivalCountry: "Spain",
            DepDate: new DateTime(2023, 9, 20, 15, 45, 0),
            DepAirport: "FRA",
            ArrivalAirport: "MAD",
            FlightClasses: new List<FlightClassModel>
            {
                new FlightClassModel(FlightClassEnum.Economy, 350, 300),
                new FlightClassModel(FlightClassEnum.Business, 1000, 60),
                new FlightClassModel(FlightClassEnum.FirstClass, 2200, 25)
            },
            DateFormat: "dd/MM/yyyy HH:mm:ss"
        ),
    };

            return flights;
        }
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
            string expectedCSV = "2,UK,France,2023-08-15 08:00:00,LHR,CDG,default,3\r\n" +
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

        

    }
}
