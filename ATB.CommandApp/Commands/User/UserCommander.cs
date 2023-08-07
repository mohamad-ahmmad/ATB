using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.User
{
    public class UserCommander : ICommander
    {
        public void Start()
        {
            CommandRegistry registry = CommandRegistry.GetInstance();
            while (true)
            {
                if (AppState.UserId == null) 
                {
                    AppState.CurrentState = State.LoginOrRegister;
                    return;
                }

                Console.WriteLine("\r\nChoose a number : ");
                Console.WriteLine("1- View all bookings.");
                Console.WriteLine("2- Book a flight.");
                Console.WriteLine("3- Search for flights.");
                Console.WriteLine("4- Modify a book.");
                Console.WriteLine("5- Cancel a book.");
                Console.WriteLine("6- Log out.");

                string? command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command))
                    continue;

                registry.GetCommand(command).Execute();

            }


        }
    }
}
