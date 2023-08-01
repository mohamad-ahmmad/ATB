using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Repositories.Flights
{
    public class FileFlightRepository : IFlightRepository
    {
        private const string _filePath = @"C:\Users\USER-M\source\repos\ATB\ATB.DA\Data\flights.csv";
        private List<FlightModel> _flights ;
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
            if (flightsStoredInFile.Length <=1)
                return flights;

            int i = 0;
            while(i < flightsStoredInFile.Length)
            {
                string flightDetails = flightsStoredInFile[i]+"\r\n";
                string flightClassesDetails = string.Empty;

                //Accumlate the csv data about the
                int flightClassesCount = int.Parse(flightDetails.Split(',')[7]);
                int offset = i;
                for(i++; i < flightClassesCount+offset+1; i++)
                    flightClassesDetails += flightsStoredInFile[i] + "\r\n";

                flights.Add(FlightModel.FromCSV(flightDetails + flightClassesDetails));               
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

        
        /// <summary>
        /// Add the provided flight to the system.
        /// </summary>
        /// <param name="flight"></param>
        /// <returns>OperationStatusEnum</returns>
        public OperationStatusEnum AddFlight(FlightModel flight)
        {
            try
            {
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
    }
}
