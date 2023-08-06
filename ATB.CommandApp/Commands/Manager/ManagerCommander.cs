using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.Manager
{
    public class ManagerCommander : ICommander
    {
        public void Start()
        {
            while (true)
            {
                if (AppState.UserId == null)
                    return;

                Console.WriteLine("Choose a number : ");
                Console.WriteLine("1- Add flights from a csv file.");
                Console.WriteLine("2- Search using filter for a flights.");
                Console.WriteLine("3- Get the model validation.");
                Console.WriteLine("4- Log out.");

                string? command = Console.ReadLine()!.ToLower();

                if (command == null) continue;

                CommandRegistry registry = CommandRegistry.GetInstance();

                registry.GetCommand(command).Execute();

            }


        }
    }
}
