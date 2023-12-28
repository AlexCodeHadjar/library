using library.Data.Models;
using Library.Data.interfaces;

namespace Library.Data.mocks
{
    public class MockPublicsher : IPublicsher
    {
        ///<summary>
        ///реализация интерфейса IPublicsher
        /// </summary>
        public IEnumerable<Publisher> Allpublicshers {
            get 
            {
                return new List<Publisher>
                {
                    new Publisher{id=1001,name="Эксмо",contacts="123123123",address="Москва" },
                    new Publisher{id=1002,name="АСТ ",contacts="123145123",address="Питер" },
                    new Publisher{id=1003,name="Просвещение ",contacts="123343123",address="Новгород" },
                };
            } 
        }
    }
}
