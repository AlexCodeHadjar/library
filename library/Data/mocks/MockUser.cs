using library.Data.Models;
using library.DataBase;
using library.Data.interfaces;

namespace library.Data.mocks
{
    ///<summary>
    ///реализация интерфейса IUser
    /// </summary>
    public class MockUser :IUser
    {
        // строка подключения
        static string connectionString = "Data Source=Catalogsdata.db";








        DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);

        public IEnumerable<User> AllUsers
        {
            get
            {
                return databaseHelper.SelectUser();
            }
        }
    }
}
