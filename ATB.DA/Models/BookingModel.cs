using ATB.DA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Models
{
    public record BookingModel(ulong UserId, ulong FlightId, FlightClassEnum FlightClass) 
    {
        /// <summary>
        /// Return a object of booking model from the CSV representation.
        /// </summary>
        /// <param name="csv"></param>
        /// <returns>BookingModel</returns>
        public static BookingModel FromCSV(string csv)
        {
            string[] bookingInfo = csv.Split(",");
            return new BookingModel
                (
                    ulong.Parse(bookingInfo[0]),
                    ulong.Parse(bookingInfo[1]),
                    (FlightClassEnum)int.Parse(bookingInfo[2])
                );
        }
        /// <summary>
        /// Return the CSV representation for the booking model
        /// </summary>
        /// <returns></returns>
        public string ToCSV()
            => $"{UserId},{FlightId},{(int)FlightClass}";

        public bool EqualsTo(BookingModel to)
            => this.UserId == to.UserId &&
               this.FlightId == to.FlightId &&
               this.FlightClass == to.FlightClass;

    };
}
