using ATB.BL.Services.Authentication;
using ATB.CommandApp.Commands.Manager;
using ATB.CommandApp.Commands.User;
using ATB.CommandApp.Util;
using ATB.DA.Models;
using ATB.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp.Commands.Entry
{
    public class LoginCommand : ICommand
    {
        private IAuthenticationServices _authService;
        public LoginCommand(IAuthenticationServices auth) 
            => _authService = auth;

        public void GetLoginData(out string email, out string password)
        {
            Console.Write("Email : ");
            email = Console.ReadLine()!;
            Console.Write("Password : ");
            password = Console.ReadLine()!;


        }
        public void Execute()
        {
            string email;
            string password;
            
          
                GetLoginData(out email, out password);
                UserModel? user;
                bool res = _authService.AuthenticateUserByEmail(email, password, out user);

                if (res)
                {
                    Console.WriteLine(ConsoleUtilites.Divider);
                    Console.WriteLine("Login Successful.");

                    ICommander nextCommander;
                    AppState.CurrentState = user!.AccessLevel == DA.Enums.AccessLevelEnum.Manager ? State.Manager : State.User;


                    if (AppState.CurrentState == State.Manager)
                        nextCommander = new ManagerCommander();
                    else
                        nextCommander = new UserCommander();

                    nextCommander.Start();
                }
                else
                    Console.WriteLine("Check your email or username.");
            

        }
    }
}
