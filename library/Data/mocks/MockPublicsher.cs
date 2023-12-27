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
                    new Publisher{id=1001,name="Warner1",contacts="123123123",address="street1" },
                    new Publisher{id=1002,name="Warner2",contacts="123145123",address="street2" },
                    new Publisher{id=1003,name="Warner3",contacts="123343123",address="street3" },
                };
            } 
        }
    }
}
