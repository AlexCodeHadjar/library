﻿using DataBaseHelperSQLite.Data.Models;

namespace library.Service.Contract
{
    public interface IRegistrationService
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
