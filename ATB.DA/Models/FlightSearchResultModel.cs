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
        );
}
