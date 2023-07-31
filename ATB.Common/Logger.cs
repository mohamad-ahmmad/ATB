using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.Common
{
    public static class Logger
    {
        public static void LogToConsole(List<ILoggable> list)
        {
            foreach (ILoggable loggable in list)
                Logger.LogToConsole(loggable);
        }

        public static void LogToConsole(ILoggable loggable)
        {
                Console.WriteLine(loggable.Log());
        }
    }
}
