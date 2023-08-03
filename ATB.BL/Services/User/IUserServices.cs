using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Services.User
{
    public interface IUserServices
    {
        public UserServicesResponsesEnum RegisterUser(UserModel user);
    }
}
