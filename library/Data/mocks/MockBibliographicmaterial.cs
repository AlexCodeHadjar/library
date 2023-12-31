using library.Data.Models;
using library.DataBase;
using library.Data.interfaces;
using System.Xml.Linq;

namespace library.Data.mocks
{
    ///<summary>
    ///реализация интерфейса IBibliographicmaterial
    /// </summary>
    public class MockBibliographicmaterial : IBibliographicmaterial
    {
        
       // private readonly IAuthor _author = new MockAuthor();
       // private readonly IPublicsher _publicsher = new MockPublicsher();

        // строка подключения
        static string connectionString = "Data Source=Catalogsdata.db";

            




        DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
        public IEnumerable<Bibliographicmaterial> AllBibliographicmaterial {
            get
            {
                return databaseHelper.SelectBibliographicmaterial();
            } 
        }
    }
}
