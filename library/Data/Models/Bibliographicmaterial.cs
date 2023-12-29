using System.Globalization;

namespace library.Data.Models
{
    public class Bibliographicmaterial
    {
        public int id { get; set; }
        ///<summary>
        ///получение id для Bibliographicmaterial
        /// </summary>

        public string name { get; set; }
        ///<summary>
        ///получение name для Bibliographicmaterial
        /// </summary>
        public string date { get; set; }
        ///<summary>
        ///получение date для Publisher
        /// </summary>
        public string img { get; set; }
        ///<summary>
        ///получение img для Publisher
        /// </summary>
        public Author author { get; set; }
        ///<summary>
        ///получение author для Bibliographicmaterial
        /// </summary>
        public Publisher publisher { get; set; }
        ///<summary>
        ///получение publisher для Bibliographicmaterial
        /// </summary>

    }
}
