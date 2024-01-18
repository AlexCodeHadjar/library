using library.Data.Models;

namespace library.Service
{
    public interface IBusinessLogicRegistratioan
    {
        /// <summary>
        /// Анутификация
        /// </summary>
        /// <param name="user">Вводимый пользователь</param>
        /// <returns></returns>
        public string Regist(User user);
        /// <summary>
        /// Авторизация 
        /// </summary>
        /// <param name="user">Вводимый пользователь</param>
        /// <returns></returns>
        public bool Authorization(User user);
    }
}
