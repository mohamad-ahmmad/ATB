using ATB.BL.Services.Flight;
using ATB.DA.Repositories.Flights;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATB.DA.Test.FlightsTests;
namespace ATB.BL.Test.Services
{
    [TestClass]
    public class FlightServicesTest
    {
        [TestMethod]
        public void AddFlights_InValidTwoErrorMessages()
        {
            string fileContent = "17,USA,UK1,10/08/2023 12:30:00,JFK,LHR++,dd/MM/yyyy HH:mm:ss,3\r\n" +
                "0,500,200\r\n" +
                "1,1500,50\r\n" +
                "2,3000,20\r\n";

            IFlightServices service = new FlightServices(new FileFlightRepository());

            List<string> errors= service.AddFlights(fileContent)!;

            Assert.IsTrue(errors.Count == 2);
            Assert.AreEqual("Arrival country must contain only text.", errors[0].Split("Message : ")[1] );
            Assert.AreEqual("Arrival airport only text allowed.", errors[1].Split("Message : ")[1]);
        }
    }
}
