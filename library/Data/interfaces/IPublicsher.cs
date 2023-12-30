using library.Data.Models;

namespace Library.Data.interfaces
{
    ///<summary>
    ///интерфейс для работы с данными типа IPublicsher
    /// </summary>
    public interface IPublicsher
    {
        ///<summary>
        ///перебор всех обьектов Allpublicshers
        /// </summary>
        public IEnumerable<Publisher> Allpublicshers { get;}
      
    }
}
