using ATB.BL.Services.Authentication;
using ATB.DA.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATB.BL.Test.Services
{
    [TestClass]
    public class AuthenticationServicesTests
    {

        [TestMethod] 
        public void AuthenticateUserById_TheUserExists()
        {  //Arrange
            var auth = new AuthenticationServices(new FileUserRepository());

            
            var userCredentials = new
            {
                Id = 1u,
                Password = "password123",
            };

            Assert.IsTrue(auth.AuthenticateUserById(userCredentials.Id, userCredentials.Password));

        }
        [TestMethod]
        public void AuthenticateUserByEmail_TheUserExists()
        {  //Arrange
            var auth = new AuthenticationServices(new FileUserRepository());


            var userCredentials = new
            {
                Email = "289115494@mail.com",
                Password = "password123",
            };

            Assert.IsTrue(auth.AuthenticateUserByEmail(userCredentials.Email, userCredentials.Password));

        }
    }
}
