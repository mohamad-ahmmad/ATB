using ATB.DA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Models
{
    public record FlightClassModel(
        FlightClassEnum FlightClass,
        int Price,
        int Capacity
        )
    {
        public string ToCSV() => $"{(int)FlightClass},{Price},{Capacity}";

        public static FlightClassModel FromCSV(string csv)
        {
            string[] fields = csv.Split(',');
            FlightClassEnum flightClass = (FlightClassEnum)int.Parse(fields[0]);
            int price = int.Parse(fields[1]);
            int capacity = int.Parse(fields[2]); 

            return new FlightClassModel(flightClass, price, capacity);
        }
    };
}
