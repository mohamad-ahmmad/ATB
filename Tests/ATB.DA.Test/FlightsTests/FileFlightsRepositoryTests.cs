using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Test.FlightsTests
{
    [TestClass]
    public class FileFlightsRepositoryTests
    {
        private const string _filePath = @"C:\Users\USER-M\source\repos\ATB\ATB.DA\Data\flights.csv";
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
        private void ClearFileContent() => File.WriteAllBytes(_filePath, new byte[] { });

        [TestMethod]
        public void AddFlight_ShouldAppendToCSVFileCorrectly()
        {
            //Erase All Content
            ClearFileContent();

            FileFlightRepository repo = new();

            FlightModel flight = GetMockFlightData()[0];

            repo.AddFlight(flight);
            
            //Act
            string[] fileContent = File.ReadAllLines(_filePath);
            string act = string.Empty;
            foreach (string line in fileContent) act += line +"\r\n";

            //Expected
            string csv = "1,USA,UK,31/07/2023 12:30:00,JFK,LHR,dd/MM/yyyy HH:mm:ss,3\r\n" +
                "0,500,200\r\n" +
                "1,1500,50\r\n" +
                "2,3000,20\r\n";

            Assert.AreEqual(act, csv);

        }
        [TestMethod]
        public void GetAllFlightsInFlightsFile_ShouldGetListOfFlightModelCorrectly()
        {
            //Erase File Content
            ClearFileContent();
            //arrange
            FileFlightRepository repo = new();


            //Actual
            List<FlightModel> flights = GetMockFlightData();

            repo.AddAllFlights(flights);

            //Expected
            List<FlightModel> expected = repo.GetAllFlights();

            Assert.AreEqual(expected.Count, flights.Count);
            for(int i =0; i < flights.Count; i++)
            {
                Assert.IsTrue( flights[i].EqualsTo(expected[i]));
            }

        }
        [TestMethod]
        public void Constructor_ShouldReadAllTheDataAndConvertItToListOfFlights()
        {
            ClearFileContent();

            var repo = new FileFlightRepository();
            List<FlightModel> list1 = GetMockFlightData();
            repo.AddAllFlights(list1);

            
            List<FlightModel> list2 = new FileFlightRepository().GetAllFlights();

            Assert.AreEqual(list1.Count, list2.Count);  
            for (int i = 0; i < list1.Count; i++)
                Assert.IsTrue(list1[i].EqualsTo(list2[i]));

        }

        [TestMethod]
        public void GetAllFlightUsingFilter_ShouldReturnTheResultSuccessfully()
        {
            //Arrange
            ClearFileContent();
            var repo = new FileFlightRepository();
            repo.AddAllFlights(GetMockFlightData());

            //Actual
            var res = repo.GetAllFlights(new FlightFilter(
                        500,
                        "USA",
                        "UK",
                        DateTime.ParseExact("31/07/2023 12:30:00", "dd/MM/yyyy HH:mm:ss", null),
                        "JFK",
                        "LHR",
                        FlightClassEnum.Economy
                        )).FirstOrDefault();

            //Expected
            var expected = new FlightModelSearchResultModel
                (
                        1,
                        "USA",
                        "UK",
                        DateTime.ParseExact("31/07/2023 12:30:00", "dd/MM/yyyy HH:mm:ss", null),
                        "JFK",
                        "LHR",
                        new FlightClassModel(FlightClassEnum.Economy, 500, 200)
                );

            Assert.AreEqual(expected, res);

        }

        [TestMethod]
        public void GetAllFlightUsingFilter_ShouldReturnEmptyList()
        {
            //Arrange
            ClearFileContent();
            var repo = new FileFlightRepository();
            repo.AddAllFlights(GetMockFlightData());

            //Actual
            var res = repo.GetAllFlights(new FlightFilter(
                        null,
                        "PAL",
                        null,
                        null,
                        null,
                        null,
                        null
                        )).FirstOrDefault();


            Assert.IsNull(res);

        }
    }
}
