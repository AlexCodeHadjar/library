using library.Data.Models;

namespace Library.Data.interfaces
{
    public interface IBibliographicmaterial
    {
        ///<summary>
        ///интерфейс для работы с данными типа Bibliographicmaterial
        /// </summary>
        public IEnumerable<Bibliographicmaterial> Allbibliographicmaterial { get; }
        ///<summary>
        ///перебор всех обьектов Bibliographicmaterial
        /// </summary>
    }
}
