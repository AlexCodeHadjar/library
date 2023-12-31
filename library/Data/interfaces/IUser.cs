using library.Data.Models;
namespace library.Data.interfaces
{
    ///<summary>
    ///интерфейс для работы с данными типа IPublicsher
    /// </summary>
    public interface IUser
    {
        ///<summary>
        ///перебор всех обьектов Allpublicshers
        /// </summary>
        public IEnumerable<User> AllUsers { get; }
    }
}
