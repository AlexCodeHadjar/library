using library.Data.Models;

namespace library.DataBase
{
    /// <summary>
    /// Интерфейс   для работы с Author в бд 
    /// </summary>
    public interface IDataBaseHelperAuthor
    {
        /// <summary>
        /// выборка Author из бд  
        /// </summary>
        /// <param name="nameAuthor"> Имя автора</param>
        /// <returns></returns>
        public IEnumerable<Author> SelectAuthor(string? nameAuthor=null);
        /// <summary>
        /// добавление Author из бд 
        /// </summary>
        /// <param name="author">Параметры автора</param>
        public void AddAuthor(Author? author=null);
        /// <summary>
        /// Удаление Author из бд 
        /// </summary>
        /// <param name="idAuthor">ID пользователя</param>
        public void DeleteAuthor(int? idAuthor=null);
        /// <summary>
        /// Обновление/изменение Author из бд 
        /// </summary>
        /// <param name="idAuthor">ID автора</param>
        /// <param name="nameAuthor">Имя автора</param>
        /// <param name="contactsAuthor">Контакты автора</param>
        /// <param name="informationAuthor">Информация автора</param>
        public void UpdateAuthor(int? idAuthor=null, string? nameAuthor=null, string? contactsAuthor=null, string? informationAuthor=null);
    }
}
