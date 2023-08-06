using ATB.BL.Services.Flight;
using ATB.CommandApp.Util;

namespace ATB.CommandApp.Commands.Manager
{
    public class AddFlightsFromFileCommand : ICommand
    {
        private readonly IFlightServices _flightServices;
        public AddFlightsFromFileCommand(IFlightServices flightServices)
            => _flightServices = flightServices;
        public void Execute()
        {
            Console.Write("File path : ");
            string? path = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(path))
                return;
            

            try
            {
                string fileContent = File.ReadAllText(path.Trim());
                Console.WriteLine(fileContent); 
               List<string>? errors = _flightServices.AddFlights(fileContent);
                Console.WriteLine();
                if (errors == null)
                {
                    Console.WriteLine("All flights added successfully.");
                    return;
                }
                 
                Console.WriteLine($"There's {errors.Count} " + (errors.Count == 1 ? "error" : "errors") + " : ");
                errors.ForEach(error => Console.WriteLine(error));

            }catch (Exception _)
            {
                Console.WriteLine("Error Message : The file path can not be accessed or doesn't exist.");
                return;
            }

            Console.WriteLine(ConsoleUtilites.Divider);
        }
    }
}
