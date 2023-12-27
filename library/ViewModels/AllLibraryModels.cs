using library.Data.Models;

namespace library.ViewModels
{
    public class AllLibraryModels
    {
        ///<summary>
        ///обьединение всех моделей для передачи предствалению
        /// </summary>
        public IEnumerable<Author> getallauthors {  get; set; }
        ///<summary>
        ///перебор всех обьектов Author
        /// </summary>

        public IEnumerable<Publisher> getallpublishers {  get; set; }
        ///<summary>
        ///перебор всех обьектов Publisher
        /// </summary>
        public IEnumerable <Bibliographicmaterial> getallBibliographicmaterial {  get; set; }
        ///<summary>
        ///перебор всех обьектов Bibliographicmaterial
        /// </summary>
    }
}
