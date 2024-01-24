using library.Data.Models;
using library.ViewModels;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;
using static library.Controllers.HomeController;
using static library.DataBase.Impl.DatabaseHelper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace library.DataBase.Impl
{

    ///<summary>
    ///класс для работы с базой данных CatalogsData
    /// </summary>// 
    public class DatabaseHelper
    {

        public const string CONNECTION_STRING = "Data Source=Catalogsdata.db";

        public string _connectionString = CONNECTION_STRING;
        public DatabaseHelper(string dbConnectionString)
        {
            _connectionString = dbConnectionString ?? CONNECTION_STRING;
        }
    }
}


