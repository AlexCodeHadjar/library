using library.Data.Models;

namespace Library.Data.interfaces
{
   public  interface IAuthor
    {
        public IEnumerable<Author> Alluthors { get; set; }
    }
}
