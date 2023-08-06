﻿using ATB.BL.Services.Authentication;
using ATB.BL.Services.Booking;
using ATB.BL.Services.Flight;
using ATB.BL.Services.User;
using ATB.CommandApp.Commands;
using ATB.CommandApp.Commands.Entry;
using ATB.DA.Repositories;
using ATB.DA.Repositories.Bookings;
using ATB.DA.Repositories.Flights;
using System;

namespace ATB.CommandApp
{
    class App
    {   //Configurations :
        public IAuthenticationServices Auth = new AuthenticationServices(new FileUserRepository());
        public IUserServices UserServices = new UserServices(new FileUserRepository());
        public IFlightServices FlightServices = new FlightServices(new FileFlightRepository());
        public IBookingServices BookingServices = new BookingServices(new FileFlightRepository(), new FileBookingRepository());
        //End

        public void Launch()
        {
            CommandRegistry registry = new(Auth, UserServices, FlightServices, BookingServices);
            EntryCommander commander = new(registry);
            commander.Start();
        }

        public static void Main(string[] args)
        {
           new App().Launch();
        }
    }
}