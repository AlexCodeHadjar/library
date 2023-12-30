using library.Data.Models;
using library.DataBase;
using Library.Data.interfaces;

namespace Library.Data.mocks
{
    ///<summary>
    ///реализация интерфейса IAuthor
    /// </summary>
    public class MockAuthor : IAuthor
    {
        // строка подключения
        static string connectionString = "Data Source=Catalogsdata.db";

        DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
        public IEnumerable<Author> AllAuthors
        {
        
            get
            {
                return databaseHelper.SelectAuthor();
            }
        }
    }
}
