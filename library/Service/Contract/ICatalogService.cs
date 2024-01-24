using library.Data.Models;
using library.DataBase;
using library.ViewModels;
using static library.Service.Impl.CatalogService;

namespace library.Service.Contract
{
    public interface ICatalogService
    {

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
        public AllLibraryModels SortLibraryModels(string nameBibliographicmaterial, string nameAuthor, string namePublisher, string date, SortBy sortBy);

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

        /// <summary>
        /// Выборка Bibliographicmaterial по введеным значениям 
        /// </summary>
        /// <param name="nameBibliographicmaterial"></param>
        /// <param name="date"></param>
        /// <param name="nameAuthor"></param>
        /// <param name="namePublisher"></param>
        /// <returns></returns>
        public IEnumerable<BibliographicMaterial> SelectBibliographicmaterial(string nameBibliographicmaterial, string date, string nameAuthor, string namePublisher);

    }
}



