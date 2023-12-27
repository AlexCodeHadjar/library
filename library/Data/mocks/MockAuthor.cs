using library.Data.Models;
using Library.Data.interfaces;

namespace Library.Data.mocks
{
    public class MockAuthor : IAuthor
    {
        ///<summary>
        ///реализация интерфейса IAuthor
        /// </summary>
        public IEnumerable<Author> AllAuthors
        {
        
            get
            {
                return new List<Author>()
                {
                    new Author{id=101,fullName="Dadasdas",contacts="+123123123",information="популярный автор1" },
                    new Author{id=102,fullName="Dadasdasasd",contacts="+123123124",information="популярный автор2" },
                    new Author{id=103,fullName="Dadasdasdasdas",contacts="+123123125",information="популярный автор3" },
                    new Author{id=104,fullName="Dadassadsadasddas",contacts="+123123126",information="популярный автор4" },
                };
            }
        }
    }
}
