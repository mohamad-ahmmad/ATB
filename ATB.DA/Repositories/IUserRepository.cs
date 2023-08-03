using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATB.DA.Models;
using ATB.DA.Enums;

namespace ATB.DA.Repositories
{
    public interface IUserRepository
    {
        public OperationStatusEnum AddUser(UserModel user);
        public UserModel? GetUser(ulong userId);
        public List<UserModel> GetAllUsers();
        public UserModel? GetUser(string email);

    }
}
