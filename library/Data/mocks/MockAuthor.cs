using library.Data.Models;
using library.DataBase;
using library.Data.interfaces;

namespace library.Data.mocks
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
        public IEnumerable<Author> SelectAuthor(string nameAuthor)
        {
            
            IEnumerable<Author> allAuthors = AllAuthors;

     
            IEnumerable<Author> filteredAuthors = allAuthors.Where(a => a.FullName == nameAuthor);

            return filteredAuthors;
        }

    }
}
