/*using library.Data.Models;

namespace library.DataBase
{
    /// <summary>
    /// Интерфейс для работы с BibliographicMaterial в бд 
    /// </summary>
    public interface IDataBaseHelperBibliographicmaterial
    {
        /// <summary>
        /// Выборка Bibliographicmaterial из бд 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BibliographicMaterial> SelectBibliographicmaterial();
        /// <summary>
        /// Выборка Bibliographicmaterial из бд с фильтром
        /// </summary>
        /// <param name="nameBibliographicmaterial">Имя </param>
        /// <param name="date"> Год издания </param>
        /// <param name="nameAuthor">Имя автора</param>
        /// <param name="namePublisher">Название Издательства</param>
        /// <returns></returns>
        public IEnumerable<BibliographicMaterial> SelectBibliographicmaterial(string nameBibliographicmaterial, string date, string nameAuthor, string namePublisher );
        /// <summary>
        /// Добавление BibliographicMaterial в бд
        /// </summary>
        /// <param name="material">Имя </param>
        public void InsertBibliographicMaterial(BibliographicMaterial material);
        /// <summary>
        /// Удаление BibliographicMaterial из бд
        /// </summary>
        /// <param name="idBibliographicmaterial">ID </param>

        public void DeleteBibliographicmaterial(int idBibliographicmaterial);
        /// <summary>
        /// Обновление/изменение BibliographicMaterial в бд 
        /// </summary>
        /// <param name="idBibliographicmaterial">ID </param>
        /// <param name="nameBibliographicmaterial"> Имя </param>
        /// <param name="nameAuthor"> Имя автора </param>
        /// <param name="namePublisher">Название издательства</param>
        /// <param name="date">Год издания</param>
        public void UpdateBibliographicmaterial(BibliographicMaterial material);

    }
}
*/