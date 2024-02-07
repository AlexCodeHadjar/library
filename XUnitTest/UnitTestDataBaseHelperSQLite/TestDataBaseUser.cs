using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTest.UnitTestDataBaseHelperSQLite
{
    public class TestDataBaseUser
    {
        public const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
        private readonly MockedServiceCollection _serviceCollection = new MockedServiceCollection();
        private readonly IDataBaseHelperModels<User> _publisherServices;
        public TestDataBaseUser()
        {
            _publisherServices = (IDataBaseHelperModels<User>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<User>));

        }
    }
}
