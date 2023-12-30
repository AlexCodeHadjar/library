using library.Data.Models;
using Library.Data.interfaces;
using library.DataBase;

namespace Library.Data.mocks
{
    ///<summary>
    ///реализация интерфейса IPublicsher
    /// </summary> 
    public class MockPublicsher : IPublicsher
    {
        
        // строка подключения
        static string connectionString = "Data Source=Catalogsdata.db";

        
        
       




        DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
  
        public IEnumerable<Publisher> Allpublicshers {
            get 
            {
                return databaseHelper.SelectPublisher();
            } 
        }
    }
}
