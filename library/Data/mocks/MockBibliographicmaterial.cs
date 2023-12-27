using library.Data.Models;
using Library.Data.interfaces;
using System.Xml.Linq;

namespace Library.Data.mocks
{
    public class MockBibliographicmaterial : IBibliographicmaterial
    {
        ///<summary>
        ///реализация интерфейса IBibliographicmaterial
        /// </summary>
        private readonly IAuthor _author = new MockAuthor();
        private readonly IPublicsher _publicsher = new MockPublicsher();
        public IEnumerable<Bibliographicmaterial> Allbibliographicmaterial {
            get
            {
                return new List<Bibliographicmaterial>
                {
                    new Bibliographicmaterial {id=1,name="Harry Potter",author = _author.AllAuthors.First(),publisher = _publicsher.Allpublicshers.Last()},
                    new Bibliographicmaterial {id=1,name="Harry Potter",author = _author.AllAuthors.Last(),publisher = _publicsher.Allpublicshers.First() },
                    new Bibliographicmaterial {id=1,name="Harry Potter",author = _author.AllAuthors.Last(),publisher = _publicsher.Allpublicshers.Last() },
                    new Bibliographicmaterial {id=1,name="Harry Potter",author = _author.AllAuthors.First(),publisher = _publicsher.Allpublicshers.Last() }
                };
            } 
        }
    }
}
