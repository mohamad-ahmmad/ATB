using ATB.BL.Services.User;
using ATB.CommandApp.Util;
using ATB.DA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.Entry
{
    public class RegisterCommand : ICommand
    {
        private IUserServices _userServices;
        public RegisterCommand(IUserServices userServices)
            => _userServices = userServices;

        public void Execute()
        {
            Console.Write("Enter your first name : ");
            string firstName = ConsoleUtilites.GetFromConsoleNotNullOrEmpty();

            Console.Write("Enter your middle name : ");
            string middleName = ConsoleUtilites.GetFromConsoleNotNullOrEmpty();

            Console.Write("Enter your last name : ");
            string lastName = ConsoleUtilites.GetFromConsoleNotNullOrEmpty();

            Console.Write("Enter your email : ");
            string email = ConsoleUtilites.GetValidEmail();

            Console.Write("Enter your password : ");
            string password = ConsoleUtilites.GetFromConsoleNotNullOrEmpty();

            UserServicesResponsesEnum res =
                _userServices.RegisterUser(new DA.Models.UserModel(
                    0,
                    firstName,
                    middleName,
                    lastName,
                    email,
                    password,
                    AccessLevelEnum.Passenger
                    ));

            if (res == UserServicesResponsesEnum.UserEmailAlreadyRegistered)
            {
                Console.WriteLine("Regiseration failed, the email already used.");
                return;
            }

            Console.WriteLine("Registered Successfully.");
            Console.WriteLine(ConsoleUtilites.Divider);
        }
    }
}
