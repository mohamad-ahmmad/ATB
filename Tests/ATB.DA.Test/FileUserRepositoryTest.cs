using ATB.DA.Enums;
using ATB.DA.Models;
using ATB.DA.Repositories;

namespace ATB.DA.Test
{
    [TestClass]
    public class FileUserRepositoryTest
    {
        private string _filePath = @"C:\Users\USER-M\source\repos\ATB\ATB.DA\Data\users.csv";

        public void EraseFileContent()=> File.WriteAllBytes(_filePath, new byte[] { });

        private List<UserModel> _fakeData = new List<UserModel>
            {
            new UserModel(1, "John", "Doe", "Smith", "john.doe@example.com", "password123", AccessLevelEnum.Manager),
            new UserModel(2, "Jane", "", "Johnson", "jane.johnson@example.com", "pass123", AccessLevelEnum.Passenger),
            new UserModel(3, "Mike", "R.", "Williams", "mike.williams@example.com", "securepwd!", AccessLevelEnum.Passenger),
            new UserModel(4, "Sarah", "K.", "Brown", "sarah.brown@example.com", "password456", AccessLevelEnum.Passenger),
            new UserModel(5, "Robert", "", "Lee", "robert.lee@example.com", "ilovegpt3", AccessLevelEnum.Passenger)
            };

        [TestMethod]
        public void AddUserTest()
        {
            EraseFileContent();

            FileUserRepository repo = new();
            UserModel user =
                new Models.UserModel(1, "John", "Doe", "Smith", "john.doe@example.com", "password123", AccessLevelEnum.Passenger);
            OperationStatusEnum act 
                = repo.AddUser(user);

            OperationStatusEnum expected = OperationStatusEnum.Success;
            
            Assert.AreEqual(expected, act);


            //Test that the file has been modified

            string fileData = File.ReadAllLines(_filePath)[0];
            Assert.AreEqual(fileData, user.ToCSV());

        }

        [TestMethod] 
        public void GetAllUserTest() 
        {
            //Arange
            EraseFileContent();

            FileUserRepository repo = new();
            repo.AddAllUsers(_fakeData);

            //Act
            List<UserModel> Actual = repo.GetAllUsers();

            //Expected
            var expect = _fakeData;


            //Assert

            for(int i = 0;i < Actual.Count; i++)
                Assert.AreEqual(expect[i], Actual[i]);
            

        }

        [TestMethod]
        public void GetUserTest() 
        { 
            //Arange
            EraseFileContent();
            
            FileUserRepository repo = new();
            repo.AddAllUsers(_fakeData);

            //Act
            UserModel? userWithId2 = _fakeData[1];
            //Expected
            UserModel? user = repo.GetUser(2);

            Assert.AreEqual(userWithId2, user);
        }

        [TestMethod]
        public void GetUserNotFoundTest()
        {
            //Arange
            EraseFileContent();
            FileUserRepository repo = new();

            //Act
            UserModel? actUserNotFound = repo.GetUser(100);

            Assert.IsNull(actUserNotFound);
        }
    }
}