using ATB.DA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Repositories.Flights
{

    /// <summary>
    /// FlightFilter is used to define an object with fields to perform lookups based on these criteria.
    /// </summary>
    /// <param name="Price"></param>
    /// <param name="DepCountry"></param>
    /// <param name="ArrivalCountry"></param>
    /// <param name="DepDate"></param>
    /// <param name="DepAirport"></param>
    /// <param name="ArrivalAirport"></param>
    /// <param name="FlightClass"></param>
    public record FlightFilter
        (
            int? Price,
            string? DepCountry,
            string? ArrivalCountry,
            DateTime? DepDate,
            string? DepAirport,
            string? ArrivalAirport,
            FlightClassEnum? FlightClass
        );
}
