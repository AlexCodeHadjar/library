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
                    new Author{id=101,fullName="Виктор Гюго",contacts="+123123123",information="французский писатель, поэт, прозаик и драматург, одна из главных фигур французского романтизма, политический и общественный деятель." },
                    new Author{id=102,fullName="Марк Твен",contacts="+123123124",information="американский писатель, юморист, журналист и общественный деятель" },
                    new Author{id=103,fullName="Александр Пушкин",contacts="+123123125",information="русский поэт, драматург и прозаик, заложивший основы русского реалистического направления, литературный критик и теоретик литературы, историк, публицист, журналист." },
                    new Author{id=104,fullName="Джоан Роулинг",contacts="+123123126",information="британская писательница, сценаристка и кинопродюсер, наиболее известная как автор серии романов о Гарри Поттере" },
                };
            }
        }
    }
}
