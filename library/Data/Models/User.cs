namespace library.Data.Models
{
    public class User
    {
        ///<summary>
        ///получение Id для User
        /// </summary>
        public int Id { get; set; }
        ///<summary>
        ///получение Login для User
        /// </summary>
        public string Login { get; set; }
        ///<summary>
        ///получение Password для User
        /// </summary>
        public string Password { get; set; }
        ///<summary>
        ///получение Admin для User
        /// </summary>
        public string Admin { get; set; }
    }
}
