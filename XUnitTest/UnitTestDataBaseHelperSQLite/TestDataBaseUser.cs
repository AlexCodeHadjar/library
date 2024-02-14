using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;

namespace XUnitTest.UnitTestDataBaseHelperSQLite
{
    public class TestDataBaseUser
    {
        public const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
        private readonly MockedServiceCollection _serviceCollection = new MockedServiceCollection();
        private readonly IDataBaseHelperModels<User> _userServices;
        public TestDataBaseUser()
        {
            _userServices = (IDataBaseHelperModels<User>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<User>));

        }
        
        [Fact]
        public void Test_Insert_User()
        {

            User user = new User() { Id = -101, Login = "Somik2022", Password = "SomikPassword", Admin = "false" };

            var countStart = _userServices.Select().Count();

            _userServices.Insert(user);


            var countEnd = _userServices.Select().Count();
            
            Assert.NotEqual(countStart, countEnd);


        }
        [Fact]
        public void Test_Insert_EmptyUser()
        {

            User user = new() { Id = -101 };



            var countStart = _userServices.Select().Count();

            _userServices.Insert(user);
            var countEnd = _userServices.Select().Count();
         



            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyPassword_User()
        {

            User user = new User() { Id = -101, Login = "Somik2022", Password = null, Admin = "false" };



            var countStart = _userServices.Select().Count();

            _userServices.Insert(user);


            var countEnd = _userServices.Select().Count();
       
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyAdmin_User()
        {

            User user = new User() { Id = -101, Login = "Somik2022", Password = "SomikPassword", Admin = null };



            var countStart = _userServices.Select().Count();

            _userServices.Insert(user);


            var countEnd = _userServices.Select().Count();
            
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyLogin_User()
        {

            User user = new User() { Id = -101, Login = null, Password = "SomikPassword", Admin = "false" };



            var countStart = _userServices.Select().Count();

            _userServices.Insert(user);


            var countEnd = _userServices.Select().Count();
         
            Assert.Equal(countStart, countEnd);

        }
        
    }
}
