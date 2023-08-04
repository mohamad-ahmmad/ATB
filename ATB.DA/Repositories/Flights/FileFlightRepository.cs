using ATB.DA.Enums;
using ATB.DA.Extensions;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ATB.DA.Repositories.Flights
{
    public class FileFlightRepository : IFlightRepository
    {
        private const string _filePath = @"C:\Users\USER-M\source\repos\ATB\ATB.DA\Data\flights.csv";
        private List<FlightModel> _flights ;
        private ulong _idSeq;
        public FileFlightRepository()
        {
            _flights = GetAllFlightsInFlightsFile();
        }


        /// <summary>
        /// Read all flights in the system.
        /// </summary>
        /// <returns>List of flights</returns>
        private List<FlightModel> GetAllFlightsInFlightsFile()
        {
            string[] flightsStoredInFile = File.ReadAllLines(_filePath);
            List<FlightModel> flights = new();

            //Return empty list.
            if (flightsStoredInFile.Length <=1)
                return flights;

            int i = 0;
            while(i < flightsStoredInFile.Length)
            {
                string flightDetails = flightsStoredInFile[i]+"\r\n";

                //Accumlate the csv data about the flight classes to flight[i]
                int flightClassesCount = int.Parse(flightDetails.Split(',')[7]);//1 - 3

                // Get all the flight classes related to the flight[i].
                string flightClassesDetails= flightsStoredInFile
                                            .SubArray(i+1,flightClassesCount)
                                            .Aggregate( (new StringBuilder()), 
                                                    (builder, classLine) => builder.AppendLine(classLine)).ToString();

                flights.Add(FlightModel.FromCSV(flightDetails + flightClassesDetails));

                //jump to the next flight.
                i += flightClassesCount+1;
            }

            return flights;
        }

        /// <summary>
        /// Add provided list of flights to the system.
        /// </summary>
        /// <param name="flights"></param>
        /// <returns>OperationStatusEnum</returns>
        public OperationStatusEnum AddAllFlights(List<FlightModel> flights)
        {
            
            foreach(FlightModel flight in flights)
            {
                 if(AddFlight(flight) == OperationStatusEnum.Failed)
                    return OperationStatusEnum.Failed;

                _flights.Add(flight);
            }

            return OperationStatusEnum.Success;
        }

        private ulong nextSeqNumFlightId()
            => (ulong)_flights.Count;

        /// <summary>
        /// Add the provided flight to the system.
        /// </summary>
        /// <param name="flight"></param>
        /// <returns>OperationStatusEnum</returns>
        public OperationStatusEnum AddFlight(FlightModel flight)
        {
            try
            {
                flight.FlightId = nextSeqNumFlightId(); 
               
                string csvFormat = flight.ToCSV();
               
                File.AppendAllText( _filePath, csvFormat);
            }catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
                return OperationStatusEnum.Failed;
            }

            return OperationStatusEnum.Success;
        }

        /// <summary>
        /// Return all the flights in the system.
        /// </summary>
        /// <returns></returns>
        public List<FlightModel> GetAllFlights()=> _flights;


        /// <summary>
        /// Return a result list of FlightModel the matches with the filter data.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<FlightSearchResultModel> GetAllFlights(FlightFilter filter)
        {
           //Filter Function used in where statement.
            Func<FlightSearchResultModel, bool> predicate = (flight) =>
            {
                bool depCountryCondition = filter.DepCountry == null || filter.DepCountry == flight.DepCountry;
                bool arrivalCountryCondition = filter.ArrivalCountry == null || filter.ArrivalCountry == flight.ArrivalCountry;
                bool depTimeCondition = filter.DepDate == null || filter.DepDate.Equals(flight.DepDate);
                bool depAirportCondition = filter.DepAirport == null || filter.DepAirport == flight.DepAirport;
                bool arrivalAirportCondition = filter.ArrivalAirport == null || filter.ArrivalAirport == flight.ArrivalAirport;
                bool flightClassCondition = filter.FlightClass == null || filter.FlightClass == flight.FlightClassModel.FlightClass;
                bool priceCondition = filter.Price == null || filter.Price == flight.FlightClassModel.Price;

                return depCountryCondition &&
                        arrivalCountryCondition &&
                        depTimeCondition &&
                        depAirportCondition &&
                        arrivalAirportCondition &&
                        flightClassCondition &&
                        priceCondition;

            };

            //flatten the flights becuase the single flight might have multiple classes.
            var res = _flights.SelectMany((flight) => flight.FlightClasses,
                                (flight, flightClass) => new FlightSearchResultModel
                                (
                                    flight.FlightId,
                                    flight.DepCountry,
                                    flight.ArrivalCountry,
                                    flight.DepDate,
                                    flight.DepAirport,
                                    flight.ArrivalAirport,
                                    flightClass
                                ));
            
            return res.Where(predicate).ToList();
        }

    }
}
