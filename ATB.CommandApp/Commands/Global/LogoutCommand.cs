using ATB.CommandApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.Global
{
    public class LogoutCommand : ICommand
    {
        public void Execute()
        {
            AppState.UserId = null;
            Console.WriteLine(ConsoleUtilites.Divider);
            Console.WriteLine("Logged out successfully.");
        }
    }
}
