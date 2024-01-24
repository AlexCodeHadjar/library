using library.Data.Models;
using library.DataBase.Contract;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace library.DataBase.ImpI
{
    public class DataBaseUser : DatabaseHelper, IDataBaseHelperModels<User>
    {
        public DataBaseUser(string _connectionString) : base(_connectionString) { }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        
        
        public void Insert(User model)
        {
            if (model.Login == null || model.Password == null || model.Admin == null)
            {
                return;

            }
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                            .UseSqlite(_connectionString)
                            .Options;

            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {

                dbContext.Users.Add(model);
                dbContext.SaveChanges();
            }
        }
  
        public IEnumerable<User> Select(User model)
        {
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                           .UseSqlite(_connectionString)
                           .Options;
            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                if (model == null)
                {
                    return dbContext.Users.ToList();
                }
                else
                {
                    return dbContext.Users.Where(a => a.Password == model.Password&& a.Login == model.Login).ToList();
                }
            }
        }
        public void Update(User model)
        {
            throw new NotImplementedException();
        }
    }
}

