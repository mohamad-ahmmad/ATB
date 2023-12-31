﻿using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories.Flights;


namespace ATB.DA.Test.FlightsTests
{
    [TestClass]
    public class FileFlightsRepositoryTests
    {
        private const string _filePath = @"C:\Users\USER-M\source\repos\ATB\ATB.DA\Data\flights.csv";
        private List<FlightModel> GetMockFlightData() => FlightMockData.GetFlightsMockData();
        private void ClearFileContent() => File.WriteAllBytes(_filePath, new byte[] { });

        [TestMethod]
        public void AddFlight_ShouldAppendToCSVFileCorrectly()
        {
            //Erase All Content
            ClearFileContent();

            FileFlightRepository repo = new();

            FlightModel flight = GetMockFlightData()[0];

            OperationStatusEnum res = repo.AddFlight(flight);
            
            

            Assert.AreEqual(OperationStatusEnum.Success ,res);

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
            var expected = new FlightSearchResultModel
                (
                        0,
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
