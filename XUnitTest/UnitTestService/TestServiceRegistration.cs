using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;
using library.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var  testUsersValueAfter = _userServices.Select().Count();
            Assert.NotEqual(testUsersValue,testUsersValueAfter);
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

    }
}
