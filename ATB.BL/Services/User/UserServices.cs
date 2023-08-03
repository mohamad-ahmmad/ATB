using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepo)
            => _userRepository = userRepo;
        public UserServicesResponsesEnum RegisterUser(UserModel user)
        {
            if(_userRepository.GetUser(user.Email) is not null)
                return UserServicesResponsesEnum.UserEmailAlreadyRegistered;

            _userRepository.AddUser(user);

            return UserServicesResponsesEnum.Success;
        }

    }
}
