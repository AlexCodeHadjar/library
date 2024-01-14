using library.Data.Models;

namespace library.DataBase
{
    /// <summary>
    /// Интерфейс для работы с Publisher в бд 
    /// </summary>
    public interface IDataBaseHelperPublisher
    {
        /// <summary>
        /// Выборка Publisher из бд  
        /// </summary>
        /// <param name="namePublisher">Название издательства</param>
        /// <returns></returns>
        public IEnumerable<Publisher> SelectPublisher(string? namePublisher = null);
        /// <summary>
        /// Добавление Publisher в бд
        /// </summary>
        /// <param name="publisher"></param>
        public void AddPublisher(Publisher? publisher=null);
        /// <summary>
        /// Обновление/изменение Publisher в бд 
        /// </summary>
        /// <param name="idPublisher">Id издательства</param>
        /// <param name="namePublisher"><Название/param>
        /// <param name="contactsPublisher">Контакты</param>
        /// <param name="addressPublisher">Адрес</param>
        public void UpdatePublisher(int? idPublisher=null, string? namePublisher = null, string? contactsPublisher = null, string? addressPublisher = null);
        /// <summary>
        /// удаление Publisher из бд
        /// </summary>
        /// <param name="idPublisher">ID издательства</param>
        public void DeletePublisher(int? idPublisher);

    }
}
