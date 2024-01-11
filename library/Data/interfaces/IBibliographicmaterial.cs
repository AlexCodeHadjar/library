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
        

    }
}
