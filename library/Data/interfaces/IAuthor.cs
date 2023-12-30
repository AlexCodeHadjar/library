﻿using library.Data.Models;

namespace Library.Data.interfaces
{
    ///<summary>
    ///интерфейс для работы с данными типа IAuthor
    /// </summary>
    public interface IAuthor
    {
        ///<summary>
        ///перебор всех обьектов Author
        /// </summary>
        public IEnumerable<Author> AllAuthors { get; }
        

    }
}
