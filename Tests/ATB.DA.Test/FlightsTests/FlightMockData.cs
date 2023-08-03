using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Test.FlightsTests
{
    public class FlightMockData
    {
        public static List<FlightModel> GetFlightsMockData()=>
        
             new List<FlightModel>
        {
        new FlightModel(
            flightId: 1,
            depCountry: "USA",
            arrivalCountry: "UK",
            depDate: new DateTime(2023, 7, 31, 12, 30, 0),
            depAirport: "JFK",
            arrivalAirport: "LHR",
            flightClasses: new List<FlightClassModel>
            {
                new FlightClassModel(FlightClassEnum.Economy, 500, 200),
                new FlightClassModel(FlightClassEnum.Business, 1500, 50),
                new FlightClassModel(FlightClassEnum.FirstClass, 3000, 20)
            },
            dateFormat: "dd/MM/yyyy HH:mm:ss"
        ),
        new FlightModel(
            flightId: 2,
            depCountry: "UK",
            arrivalCountry: "France",
            depDate: new DateTime(2023, 8, 15, 8, 0, 0),
            depAirport: "LHR",
            arrivalAirport: "CDG",
            flightClasses: new List<FlightClassModel>
            {
                new FlightClassModel(FlightClassEnum.Economy, 400, 250),
                new FlightClassModel(FlightClassEnum.Business, 1200, 40),
                new FlightClassModel(FlightClassEnum.FirstClass, 2500, 15)
            },
            null
        ),
        new FlightModel(
            flightId: 3,
            depCountry: "Germany",
            arrivalCountry: "Spain",
            depDate: new DateTime(2023, 9, 20, 15, 45, 0),
            depAirport: "FRA",
            arrivalAirport: "MAD",
            flightClasses: new List<FlightClassModel>
            {
                new FlightClassModel(FlightClassEnum.Economy, 350, 300),
                new FlightClassModel(FlightClassEnum.Business, 1000, 60),
                new FlightClassModel(FlightClassEnum.FirstClass, 2200, 25)
            },
            dateFormat: "dd/MM/yyyy HH:mm:ss"
        ),
    };
        }
}
