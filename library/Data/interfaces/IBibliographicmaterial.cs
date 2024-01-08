using library.Data.Models;

namespace library.Data.interfaces
{
    ///<summary>
    ///интерфейс для работы с данными типа Bibliographicmaterial
    /// </summary>
    public interface IBibliographicmaterial
    {
        ///<summary>
        ///перебор всех обьектов Bibliographicmaterial
        /// </summary>

        public IEnumerable<Bibliographicmaterial> AllBibliographicmaterial { get; }
        public IEnumerable<Bibliographicmaterial> SelectBibliographicmaterial(string nameBibliographicmaterial, string date, string nameAuthor, string namePublisher);


    }
}
