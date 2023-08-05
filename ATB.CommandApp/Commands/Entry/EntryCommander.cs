using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.Entry
{
    public class EntryCommander : ICommander
    {
        private readonly CommandRegistry registry;
        public EntryCommander(CommandRegistry reg)
            => registry = reg;


        public void Start()
        {
            while (true)
            {
                Console.WriteLine("Choose a number : ");
                Console.WriteLine("1- Login");
                Console.WriteLine("2- Register");
                Console.WriteLine("Or just type 'exit' to exit the application.");
                Console.WriteLine();

                string? command = Console.ReadLine()!.ToLower();

                if (command == null) continue;
                registry.GetCommand(command).Execute();

                Console.WriteLine();
            }
        }

    }
}
