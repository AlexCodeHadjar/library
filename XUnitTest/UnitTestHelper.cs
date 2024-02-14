using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;
using XUnitTest;


namespace XUnitTestProject
{
    public class UnitTestHelper
    {

     
       // public const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
       // public const string CONNECTION_STRING = "Data Source=C:\\Users\\user\\source\\repos\\library\\Catalogsdata.db";

        public const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
        private readonly MockedServiceCollection _serviceCollection = new MockedServiceCollection();


        private readonly IDataBaseHelperModels<Author> _authorServices;
        public UnitTestHelper()
        {
            _authorServices = (IDataBaseHelperModels<Author>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<Author>));



        }
    }
}
