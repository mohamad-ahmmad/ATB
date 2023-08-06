using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Models
{
    public record FlightSearchResultModel
        (
            ulong FlightId,
            string DepCountry,
            string ArrivalCountry,
            DateTime DepDate,
            string DepAirport,
            string ArrivalAirport,
            FlightClassModel FlightClassModel
        )
    {
        public override string ToString()
        =>
             $"Flight id : {FlightId}\r\n" +
                    $"Departure country : {DepCountry}\r\n" +
                    $"Arrival country : {ArrivalCountry}\r\n" +
                    $"Departure date : {DepDate}\r\n" +
                    $"Departure airport : {DepAirport}\r\n" +
                    $"Arrival Airport : {ArrivalAirport}\r\n" +
                    $"{FlightClassModel}\r\n";
        
    }
}
