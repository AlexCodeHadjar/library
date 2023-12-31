namespace library.Data.Models
{
    public class Bibliographicmaterial
    {
        ///<summary>
        ///получение id для Bibliographicmaterial
        /// </summary>
        public int Id { get; set; }

        ///<summary>
        ///получение name для Bibliographicmaterial
        /// </summary>
        public string Name { get; set; }
        ///<summary>
        ///получение date для Publisher
        /// </summary>
        public string Date { get; set; }
        ///<summary>
        ///получение img для Publisher
        /// </summary>
        public string Img { get; set; }
        ///<summary>
        ///получение author для Bibliographicmaterial
        /// </summary>
        public Author Author { get; set; }
        ///<summary>
        ///получение publisher для Bibliographicmaterial
        /// </summary>
        public Publisher Publisher { get; set; }
        

    }
}
