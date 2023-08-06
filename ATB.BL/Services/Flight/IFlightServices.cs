using ATB.DA.Models;
using ATB.DA.Repositories.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Services.Flight
{
    public interface IFlightServices
    {
        public List<string>? AddFlights(string fileContent);
        public string GetValidationModel();
        public List<FlightSearchResultModel> GetFlightsUsingFilter(FlightFilter filter);
        public List<FlightSearchResultModel> GetFlightsUsingFilter(List<FlightModel> flights ,FlightFilter filter);
    }
}
