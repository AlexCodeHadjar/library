using library.Data.Models;

namespace library.DataBase
{
    public interface IDataBaseHelperUser
    {
        public void AddUser(User? material=null);
        public IEnumerable<User> SelectUser();
    }
}
