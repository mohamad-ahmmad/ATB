using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Services.Authentication
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IUserRepository _userRepository;
        public AuthenticationServices(IUserRepository userRepo) => _userRepository = userRepo;


        public bool AuthenticateUserByEmail(string email, string password)
        {
            UserModel? user = _userRepository.GetUser(email);
            return user is not null && user.Password == password;
        }


        public bool AuthenticateUserById(ulong id, string password)
        {
            UserModel? user =_userRepository.GetUser(id);
            return user is not null &&
                user.Password == password;
        }
    }
}
