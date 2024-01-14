using library.Data.Models;

namespace library.ViewModels
{
    ///<summary>
    ///обьединение всех моделей для передачи предствалению
    /// </summary>
    public class AllLibraryModels
    {
        ///<summary>
        ///перебор всех обьектов Author
        /// </summary>
        public IEnumerable<Author>AllAuthors {  get; set; }
        ///<summary>
        ///перебор всех обьектов Publisher
        /// </summary>
        public IEnumerable<Publisher>AllPublishers {  get; set; }
        ///<summary>
        ///перебор всех обьектов Bibliographicmaterial
        /// </summary>
        public IEnumerable <BibliographicMaterial> AllBibliographicmaterial {  get; set; }
        public IEnumerable <User> AllUsers {  get; set; }


    }
}
