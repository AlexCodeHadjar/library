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
                    new Bibliographicmaterial {id=1,name="Harry Potter",author = _author.AllAuthors.First(),publisher = _publicsher.Allpublicshers.Last(),date="10.12.2015",img="/img/pict1.jpg"},
                    new Bibliographicmaterial {id=2,name="Собор Парижской Богоматери",author = _author.AllAuthors.Last(),publisher = _publicsher.Allpublicshers.First(),date="9.10.2007",img="/img/pict1.jpg" },
                    new Bibliographicmaterial {id=3,name="Дневник Анны Франк",author = _author.AllAuthors.Last(),publisher = _publicsher.Allpublicshers.Last(),date="12.02.2003",img="/img/pict1.jpg" },
                    new Bibliographicmaterial {id=4,name="Атлант расправил плечи",author = _author.AllAuthors.First(),publisher = _publicsher.Allpublicshers.Last(),date="2.05.2005",img="/img/pict1.jpg" }
                };
            } 
        }
    }
}
