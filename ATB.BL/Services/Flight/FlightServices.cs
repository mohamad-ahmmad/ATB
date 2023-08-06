using ATB.DA.Extensions;
using ATB.DA.Models;
using ATB.DA.Repositories.Flights;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Services.Flight
{
    public class FlightServices : IFlightServices
    {

        private readonly IFlightRepository _flightRepository;

        public FlightServices(IFlightRepository flightRepository)
            => _flightRepository = flightRepository;
        /// <summary>
        /// Add all flight inside the provided file content.
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns>List of errors or null if successfully added</returns>
        public List<string>? AddFlights(string fileContent)
        {
            List<string>? errorMessages = null;
            List<FlightModel> flights = new();

            string[] lines = fileContent.Trim('\n').Split("\r\n");
            int i = 0;
            bool isValid = true;
            while (i < lines.Length)
            {

                //Accumlate the csv data about the flight classes to flight[i]

                int flightClassesCount = int.Parse(lines[i].Split(',')[7]);//1 - 3

                // Get all the flight classes related to the flight[i].
                string flightClassesDetails = lines
                                            .SubArray(i + 1, flightClassesCount)
                                            .Aggregate((new StringBuilder()),
                                                    (builder, classLine) => builder.AppendLine(classLine)).ToString();

                FlightModel flight = FlightModel.FromCSV(lines[i]+ "\r\n" + flightClassesDetails);

                List<ValidationResult> errors = new();
                ValidationContext context = new(flight);
                isValid = isValid && Validator.TryValidateObject(flight,context, errors, true);

                if(!isValid)
                {
                    //Write join 
                    errorMessages ??= new List<string>();
                    errors.ForEach(
                        error => errorMessages.Add($"Error in {flight.FlightId}ID Flight.\r\nMessage : {error.ErrorMessage}"));
                }
                
                flights.Add(flight);
                //jump to the next flight.
                i += flightClassesCount + 1;
            }

            if (isValid)
                _flightRepository.AddAllFlights(flights);

            return errorMessages;
        }

        public List<FlightSearchResultModel> GetFlightsUsingFilter(FlightFilter filter)
            => _flightRepository.GetAllFlights(filter);

        public  List<FlightSearchResultModel> GetFlightsUsingFilter(List<FlightModel> flights, FlightFilter filter)
            => _flightRepository.GetAllFlights(flights,filter);

        public string GetValidationModel()
        {
            throw new NotImplementedException();
        }
    }
}
