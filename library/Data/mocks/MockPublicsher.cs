using library.Data.Models;
using library.Data.interfaces;
using library.DataBase;

namespace library.Data.mocks
{
    ///<summary>
    ///реализация интерфейса IPublicsher
    /// </summary> 
    public class MockPublicsher : IPublicsher
    {
        
        // строка подключения
        static string connectionString = "Data Source=Catalogsdata.db";

        
        
       




        DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
  
        public IEnumerable<Publisher> AllPublicshers {
            get 
            {
                return databaseHelper.SelectPublisher();
            } 
        }
    }
}
