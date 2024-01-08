using library.Data.Models;

namespace library.Data.interfaces
{
    ///<summary>
    ///интерфейс для работы с данными типа IPublicsher
    /// </summary>
    public interface IPublicsher
    {
        ///<summary>
        ///перебор всех обьектов Allpublicshers
        /// </summary>
        public IEnumerable<Publisher> AllPublicshers { get;}
        public IEnumerable<Publisher> SelectPublisher(string namePublisher);



    }
}
