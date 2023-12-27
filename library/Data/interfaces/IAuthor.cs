using library.Data.Models;

namespace Library.Data.interfaces
{
   public  interface IAuthor
    {
        ///<summary>
        ///интерфейс для работы с данными типа IAuthor
        /// </summary>
        public IEnumerable<Author> AllAuthors { get; }
        ///<summary>
        ///перебор всех обьектов Author
        /// </summary>

    }
}
