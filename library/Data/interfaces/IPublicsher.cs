using library.Data.Models;

namespace Library.Data.interfaces
{
    public interface IPublicsher
    {
        ///<summary>
        ///интерфейс для работы с данными типа IPublicsher
        /// </summary>
        public IEnumerable<Publisher> Allpublicshers { get;}
        ///<summary>
        ///перебор всех обьектов Allpublicshers
        /// </summary>
    }
}
