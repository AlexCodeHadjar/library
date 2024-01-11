using library.Data.Models;

namespace library.DataBase
{
    public interface IDataBaseHelperAuthor
    {
        public IEnumerable<Author> SelectAuthor(string? nameAuthor=null);
        public void AddAuthor(Author? author=null);
        public void DeleteAuthor(int? idAuthor=null);
        public void UpdateAuthor(int? idAuthor=null, string? nameAuthor=null, string? contactsAuthor=null, string? informationAuthor=null);
    }
}
