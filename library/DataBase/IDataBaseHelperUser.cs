using library.Data.Models;

namespace library.DataBase
{
    /// <summary>
    /// Мнтерфейс для работы с User
    /// </summary>
    public interface IDataBaseHelperUser
    {
        /// <summary>
        /// Добавление User в бд 
        /// </summary>
        /// <param name="material"></param>
        public void AddUser(User? material=null);
        /// <summary>
        /// Выборка User из бд
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> SelectUser();
    }
}
