/*using library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace library.DataBase
{
    /// <summary>
    ///Интерфейс для работы с выводимой информацией контролеров
    /// </summary>
    public interface IDataBaseHelperLibraryCatolog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AllLibraryModels StartLibraryModels();
        /// <summary>
        /// Сортировка каталога обьектов Bibliographicmaterial по вводимым данным 
        /// </summary>
        /// <param name="nameBibliographicmaterial">Название</param>
        /// <param name="nameAuthor">Имя автора</param>
        /// <param name="namePublisher">Название издательства</param>
        /// <param name="date">Год издательства</param>
        /// <param name="sortBy">Тип сортировки</param>
        /// <returns></returns>
        public AllLibraryModels SortLibraryModels(string? nameBibliographicmaterial = null, string? nameAuthor = null, string? namePublisher = null, string? date = null, DatabaseHelper.SortBy? sortBy = null);
        /// <summary>
        /// Страница информации о Bibliographicmaterial для пользователя
        /// </summary>
        /// <param name="materialId"> ID</param>
        /// <returns></returns>
        public AllLibraryModels PageBibliographicmaterial(int materialId);
        /// <summary>
        /// Страница информации о Bibliographicmaterial для админа
        /// </summary>
        /// <param name="materialId">ID</param>
        /// <returns></returns>
        public AllLibraryModels PageBibliographicmaterialAdmin(int materialId);
    }
}
*/