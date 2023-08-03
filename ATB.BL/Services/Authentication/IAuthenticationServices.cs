using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Services.Authentication
{
    public interface IAuthenticationServices
    {
        public bool AuthenticateUserByEmail(string email, string password);
        public bool AuthenticateUserById(ulong id, string passwrd);
    }
}
