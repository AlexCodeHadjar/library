namespace DataBaseHelperSQLite.DataBase.Impl
{

    ///<summary>
    ///класс для работы с базой данных CatalogsData
    /// </summary>// 
    public class DatabaseHelper
    {
        public const string CONNECTION_STRING = "Data Source=Catalogsdata.db";
        
        
        //public const string CONNECTION_STRING = "Data Source=Catalogsdata.db";

        public string _connectionString = CONNECTION_STRING;
        public DatabaseHelper(string dbConnectionString)
        {
            _connectionString = dbConnectionString ?? CONNECTION_STRING;
        }
    }
}


