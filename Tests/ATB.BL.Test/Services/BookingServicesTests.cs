using ATB.BL.Services.Booking;
using ATB.DA.Models;
using ATB.DA.Repositories.Bookings;
using ATB.DA.Repositories.Flights;
using ATB.DA.Test.FlightsTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Test.Services
{
    [TestClass]
    public class BookingServicesTests
    {

        //This test should be done after ATB.DA.Tests otherway it will crash.
        [TestMethod] 
        public void GetAllBookings_GetTheBookedFlightsSuccessfully() 
        {

            //Arrange
            IBookingServices service = new BookingServices(new FileFlightRepository(), new FileBookingRepository());

            //Act
            List<FlightModel> flights = service.GetAllBookings(1);


            //Expected
            string[] expectedCSV = new string[] 
            {
                "1,UK,France,2023-08-15 08:00:00,LHR,CDG,default,1\r\n" +
                "1,1200,40\r\n",
                "0,USA,UK,31/07/2023 12:30:00,JFK,LHR,dd/MM/yyyy HH:mm:ss,1\r\n" +
                "2,3000,20\r\n"
            };

            Assert.AreEqual(expectedCSV[0], flights[0].ToCSV());
            Assert.AreEqual(expectedCSV[1], flights[1].ToCSV());

        }

        [TestMethod]
        public void GetAllBookings_GetFightsOfUnBookedUser()
        {
            //Arrange
            IBookingServices service = new BookingServices(new FileFlightRepository(), new FileBookingRepository());

            //Act
            List<FlightModel> flights = service.GetAllBookings(6);

            Assert.IsTrue(flights.Count == 0);

        }

    }
}
