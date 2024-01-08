using library.Data.Models;
using library.DataBase;
using library.Data.interfaces;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Policy;
using System.Linq;

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

        /* public IEnumerable<Bibliographicmaterial> SelectBibliographicmaterial(string nameBibliographicmaterial, string date)
         {

             IEnumerable<Bibliographicmaterial> allBibliographicmaterial = AllBibliographicmaterial;
             IEnumerable<Bibliographicmaterial> filteredBibliographicmaterial;
             if (nameBibliographicmaterial == null && date == null)
             {
                 filteredBibliographicmaterial = AllBibliographicmaterial;
             }
             else if (nameBibliographicmaterial != null && date == null)
             {
                 filteredBibliographicmaterial = allBibliographicmaterial.Where(a => a.Name == nameBibliographicmaterial);
             }
             else if (nameBibliographicmaterial == null && date != null)
             {
                 filteredBibliographicmaterial = allBibliographicmaterial.Where(a => a.Date == date);
             }
             else
             {
                 filteredBibliographicmaterial = allBibliographicmaterial.Where(a => a.Name == nameBibliographicmaterial && a.Date == date);
             }



             return filteredBibliographicmaterial;
         }*/
        public IEnumerable<Bibliographicmaterial> SelectBibliographicmaterial(string nameBibliographicmaterial, string date, string nameAuthor, string namePublisher)
        {
            IEnumerable<Bibliographicmaterial> allBibliographicmaterial = AllBibliographicmaterial;
            IEnumerable<Bibliographicmaterial> filteredBibliographicmaterial = allBibliographicmaterial;

            if (!string.IsNullOrEmpty(nameBibliographicmaterial))
            {
                filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => a.Name == nameBibliographicmaterial);
            }

            if (!string.IsNullOrEmpty(date))
            {
                filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => a.Date == date);
            }

            if (!string.IsNullOrEmpty(nameAuthor))
            {
                var authors = databaseHelper.SelectAuthor(nameAuthor).Select(a => a.Id);
                filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => authors.Contains(a.Author.Id));
            }

            if (!string.IsNullOrEmpty(namePublisher))
            {
                var publishers = databaseHelper.SelectPublisher(namePublisher).Select(a => a.Id);
                filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => publishers.Contains(a.Publisher.Id));
            }

            return filteredBibliographicmaterial;
        }

    }
}
