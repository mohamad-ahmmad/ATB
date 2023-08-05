using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.Global
{
    public class UnknownOptionCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Unkown option please try again.");
        }
    }
}
