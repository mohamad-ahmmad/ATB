using ATB.BL.Services.Flight;
using ATB.CommandApp.Util;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.Manager
{
    public class GetModelValidationCommand : ICommand
    {
        private IFlightServices _flightServices;

        public GetModelValidationCommand(IFlightServices flightServices)
            => this._flightServices = flightServices;
        

        public void Execute()
        {
            Console.WriteLine(ConsoleUtilites.Divider);
            List<string> list = FlightModel.GetFlightModelFieldDetails();
            foreach (var item in list)
                Console.WriteLine(item);
            Console.WriteLine(ConsoleUtilites.Divider);

        }
    }
}
