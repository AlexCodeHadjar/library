using library.Data.Models;
using Microsoft.AspNetCore.Mvc;

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
        /// <summary>
        /// Анутификация
        /// </summary>
        /// <param name="user">Вводимый пользователь</param>
        /// <param name="login">Проверка пользователей на совпадения</param>
        /// <returns></returns>
        public string Regist(User user,User login);
        /// <summary>
        /// Авторизация 
        /// </summary>
        /// <param name="user">Вводимый пользователь</param>
        /// <param name="userExists">Проверка пользователей на совпадения</param>
        /// <returns></returns>
        public bool Authorization(User user,bool userExists);
    }
}
