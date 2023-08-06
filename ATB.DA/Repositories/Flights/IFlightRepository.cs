﻿using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Repositories.Flights
{
    public interface IFlightRepository
    {
        public OperationStatusEnum AddFlight(FlightModel flight);
        public OperationStatusEnum AddAllFlights(List<FlightModel> flights);
        public List<FlightModel> GetAllFlights();
        public List<FlightSearchResultModel> GetAllFlights(FlightFilter filter);
        public List<FlightSearchResultModel> GetAllFlights(List<FlightModel> flights, FlightFilter filter);
    }
}
