using library.Data.Models;
using library.DataBase;
using library.ViewModels;

namespace library.BusinessLogic
{
    public interface IBusinessLogicCatalog
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
            public AllLibraryModels SortLibraryModels(string nameBibliographicmaterial , string nameAuthor , string namePublisher , string date , DatabaseHelper.SortBy sortBy );
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
            public IEnumerable<BibliographicMaterial> SelectBibliographicmaterial(string nameBibliographicmaterial, string date, string nameAuthor, string namePublisher);

    }
    }



