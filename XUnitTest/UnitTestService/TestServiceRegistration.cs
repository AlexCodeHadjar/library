using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;
using library.Service.Contract;

namespace XUnitTest.UnitTestService
{
    public class TestServiceRegistration
    {

        private readonly MockedServiceCollection _serviceCollection = new MockedServiceCollection();
        private readonly IRegistrationService _registrationServices;
        private readonly IDataBaseHelperModels<User> _userServices;

        public TestServiceRegistration()
        {
            _registrationServices = _serviceCollection.GetRequiredService<IRegistrationService>();
            _userServices = (IDataBaseHelperModels<User>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<User>));
        }

        [Fact]
        public void Test_ICatalogService()
        {
            var testUsersValue = _userServices.Select().Count();
            User testUser = new() { Admin = "true", Password = "newPassword", Login = "newLogin" };
            _registrationServices.Authorization(testUser);
            var testUsersValueAfter = _userServices.Select().Count();
            Assert.NotEqual(testUsersValue, testUsersValueAfter);
        }
        [Fact]
        public void Test_ICatalogService_EmptyAdmin()
        {
            var testUsersValue = _userServices.Select().Count();
            User testUser = new() { Admin = "true", Password = "newPassword", Login = "newLogin" };
            _registrationServices.Authorization(testUser);
            var testUsersValueAfter = _userServices.Select().Count();
            Assert.Equal(testUsersValue, testUsersValueAfter);
        }
        [Fact]
        public void Test_ICatalogService_EmptyPassword()
        {
            var testUsersValue = _userServices.Select().Count();
            User testUser = new() { Admin = "true", Password = null, Login = "newLogin" };
            _registrationServices.Authorization(testUser);
            var testUsersValueAfter = _userServices.Select().Count();
            Assert.Equal(testUsersValue, testUsersValueAfter);
        }
        [Fact]
        public void Test_ICatalogService_EmptyLogin()
        {
            var testUsersValue = _userServices.Select().Count();
            User testUser = new() { Admin = "true", Password = "newPassword", Login = null };
            _registrationServices.Authorization(testUser);
            var testUsersValueAfter = _userServices.Select().Count();
            Assert.Equal(testUsersValue, testUsersValueAfter);
        }
        [Fact]
        public void Test_Regist_AdminTrue()
        {
            User testUser = new() { Admin = "true", Password = "newPassword", Login = "newLogin" };
            var valueUserTest = _registrationServices.Regist(testUser);
            Assert.True(valueUserTest == "true");

        }
        [Fact]
        public void Test_Regist_AdminFalse()
        {
            User testUser = new() { Admin = "false", Password = "newPassword", Login = "newLogin" };
            var valueUserTest = _registrationServices.Regist(testUser);
            Assert.True(valueUserTest != "false");

        }
        [Fact]
        public void Test_Regist_WrongLogin()
        {
            User testUser = new() { Admin = "true", Password = "newPassword", Login = null };
            var valueUserTest = _registrationServices.Regist(testUser);
            Assert.True(valueUserTest == " ");

        }
        [Fact]
        public void Test_Regist_WrongPassword()
        {
            User testUser = new() { Admin = "true", Password = null, Login = "newLogin" };
            var valueUserTest = _registrationServices.Regist(testUser);
            Assert.True(valueUserTest == " ");

        }


    }
}
