using ATB.BL.Services.Authentication;
using ATB.BL.Services.Booking;
using ATB.BL.Services.Flight;
using ATB.BL.Services.User;
using ATB.CommandApp.Commands.Entry;
using ATB.CommandApp.Commands.Global;
using ATB.CommandApp.Commands.Manager;
using ATB.CommandApp.Commands.User;

namespace ATB.CommandApp.Commands
{
    public class CommandRegistry
    {
        private readonly IAuthenticationServices _authSerivces;
        private readonly IUserServices _userServices;
        private readonly IFlightServices _flightServices;
        private readonly IBookingServices _bookingServices;

        private static CommandRegistry? instance = null;

        private CommandRegistry(IAuthenticationServices authSerivces, IUserServices userServices, IFlightServices flightServices, IBookingServices bookingServices)
            => (_authSerivces, _userServices, _flightServices, _bookingServices) = (authSerivces, userServices, flightServices, bookingServices);


        public static CommandRegistry GetInstance()
        {
            instance ??= new CommandRegistry(App.Auth, App.UserServices, App.FlightServices, App.BookingServices);
            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dictionary<Tuple<string, State>, ICommand> GetCommands()
        {
            var map = new Dictionary<Tuple<string, State>, ICommand>();
            
            map.Add(new Tuple<string, State>("1", State.LoginOrRegister), new LoginCommand(_authSerivces));
            map.Add(new Tuple<string, State>("2", State.LoginOrRegister), new RegisterCommand(_userServices));
            map.Add(new Tuple<string, State>("exit", State.LoginOrRegister), new ExitCommand());

            map.Add(new Tuple<string, State> ("1", State.Manager), new AddFlightsFromFileCommand(_flightServices));
            map.Add(new Tuple<string, State> ("2", State.Manager), new FilterSearchCommand(_flightServices, _bookingServices));
            map.Add(new Tuple<string, State> ("3", State.Manager), new GetModelValidationCommand(_flightServices));
            map.Add(new Tuple<string, State> ("4", State.Manager), logoutCommand);
            return map;
        }
        private Dictionary<Tuple<string, State>, ICommand>? _commands = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public ICommand GetCommand(string commandName)
        {
            _commands ??= GetCommands();

            bool res = _commands.TryGetValue(new Tuple<string,State>(commandName, AppState.CurrentState), out ICommand? command);

            if(!res)
                return new UnknownOptionCommand();
            

            return command!;
        }

    }
}
