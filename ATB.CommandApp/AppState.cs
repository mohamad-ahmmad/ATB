using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ATB.CommandApp
{
    public enum State
    {
        LoginOrRegister,
        Manager,
        User
    }
    public class AppState
    {
        public static ulong? UserId = null;
        public static State CurrentState = State.LoginOrRegister; 
    }
}
