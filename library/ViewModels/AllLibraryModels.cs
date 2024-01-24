using DataBaseHelperSQLite.Data.Models;

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
        public IEnumerable<Author> AllAuthors { get; set; }

        ///<summary>
        ///перебор всех обьектов Publisher
        /// </summary>
        public IEnumerable<Publisher> AllPublishers { get; set; }

        ///<summary>
        ///перебор всех обьектов Bibliographicmaterial
        /// </summary>
        public IEnumerable<BibliographicMaterial> AllBibliographicmaterial { get; set; }

        ///<summary>
        ///перебор всех обьектов User
        /// </summary>
        public IEnumerable<User> AllUsers { get; set; }

        ///<summary>
        ///перебор всех картинок 
        /// </summary>
        public IEnumerable<BibliographicMaterial> AllImgs { get; set; }



    }
}
