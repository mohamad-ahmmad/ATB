using ATB.DA.Enums;
using ATB.DA.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Repositories
{
    public class FileUserRepository : IUserRepository
    {
        const string _filePath = @"C:\Users\USER-M\source\repos\ATB\ATB.DA\Data\users.csv";
        private List<UserModel> _users = null!;

        public FileUserRepository() 
        {
            _users = new List<UserModel>();
            string[] usersText = File.ReadAllLines(_filePath);

            foreach (string line in usersText)
            _users.Add(UserModel.FromCSV(line));
        }


        /// <summary>
        /// Add all the users in the given list to the file
        /// </summary>
        /// <param name="users"></param>
        /// <returns>OperationStatusEnum</returns>
        public OperationStatusEnum AddAllUsers(IEnumerable<UserModel> users)
        {
            foreach (UserModel user in users)
            {
                OperationStatusEnum res = AddUser(user);

                if (res == OperationStatusEnum.Failed)
                    return OperationStatusEnum.Failed;
            }

            return OperationStatusEnum.Success;
        }


        /// <summary>
        /// Add a user to the file.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns
        public OperationStatusEnum AddUser(UserModel user)
        {
            
            if(AddUserToFile(user.ToCSV()) == OperationStatusEnum.Failed)
                return OperationStatusEnum.Failed;
            
            _users.Add(user);
            return OperationStatusEnum.Success;
        }

        /// <summary>
        /// Append the user to the users csv file.
        /// </summary>
        /// <param name="csvFormat"></param>
        /// <returns>OperationStatusEnum</returns>
        private static OperationStatusEnum AddUserToFile(string csvFormat)
        {
            try
            {
                File.AppendAllText(_filePath, csvFormat + "\r\n");
                return OperationStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
                return OperationStatusEnum.Failed;
            }
        }

        /// <summary>
        /// Return all users in the file
        /// </summary>
        /// <returns>List<UserModel></returns>
        public List<UserModel> GetAllUsers() => _users;

        /// <summary>
        /// Get a user with the specified id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserModel?</returns>
        public UserModel? GetUser(ulong userId) 
            => _users.Where(user => user.UserId == userId).FirstOrDefault();

           
    }
}
