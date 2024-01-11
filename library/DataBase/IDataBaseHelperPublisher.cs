using library.Data.Models;

namespace library.DataBase
{
    public interface IDataBaseHelperPublisher
    {
        public IEnumerable<Publisher> SelectPublisher(string? namePublisher = null);
        public void AddPublisher(Publisher? publisher=null);
        public void UpdatePublisher(int? idPublisher=null, string? namePublisher = null, string? contactsPublisher = null, string? addressPublisher = null);
        public void DeletePublisher(int? idPublisher);

    }
}
