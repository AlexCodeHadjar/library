using library.Data.Models;
using library.DataBase;
using Library.Data.interfaces;
using System.Xml.Linq;

namespace Library.Data.mocks
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
        public IEnumerable<Bibliographicmaterial> Allbibliographicmaterial {
            get
            {
                return databaseHelper.SelectBibliographicmaterial();
            } 
        }
    }
}
