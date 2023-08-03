using ATB.BL.Services.User;
using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Test.Services
{

    [TestClass]
    public class UserServicesTests 
    {
        [TestMethod]
        public void RegisterUser_RegistrationShouldBeSuccessfully()
        {
            IUserServices service = new UserServices(new FileUserRepository());

            var userRegisterResponse = service.RegisterUser
                (
                new UserModel(1, "mohammad", "ahmad", "last", $"{new Random().Next()}@mail.com", "password123", AccessLevelEnum.Manager)
                );

            Assert.AreEqual(UserServicesResponsesEnum.Success, userRegisterResponse);

        }
        [TestMethod]
        public void RegisterUser_RegisterWithAlreadyRegisteredEmail()
        {
            //Arrange
            IUserServices service = new UserServices(new FileUserRepository());
            //Actual
            var userRegisterResponse = service.RegisterUser
                (
                new UserModel(1, "mohammad", "ahmad", "last", "robert.lee@example.com", "password123", AccessLevelEnum.Manager)
                );

            //Assert
            Assert.AreEqual(UserServicesResponsesEnum.UserEmailAlreadyRegistered, userRegisterResponse);
        }
    }
}
