﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace library.Data.Models
{
    /// <summary>
    /// Модель пользователя User
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Логин
        /// </summary>

        public string Login { get; set; }
        /// <summary>
        /// пароль
        /// </summary>

        public string Password { get; set; }
        /// <summary>
        /// Статус админа
        /// </summary>

        public string Admin { get; set; }
    }
}