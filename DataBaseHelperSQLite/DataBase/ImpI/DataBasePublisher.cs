using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;
using Microsoft.EntityFrameworkCore;

namespace DataBaseHelperSQLite.DataBase.Impl
{
    public class DataBasePublisher : DatabaseHelper, IDataBaseHelperModels<Publisher>
    {
        public DataBasePublisher(string dbConnectionString) : base(dbConnectionString) { }
        
        public void Delete(int id)
        {
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                            .UseSqlite(_connectionString)
                            .Options;

            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                var publisher = dbContext.Publishers.FirstOrDefault(a => a.Id == id);
                if (publisher != null)
                {
                    if (publisher.BibliographicMaterials.Any())
                    {

                        return;
                    }

                    dbContext.Publishers.Remove(publisher);
                    dbContext.SaveChanges();
                }
            }
        }

        public void Insert(Publisher model)
        {
            if (model.Name == null || model.Contacts == null || model.Address == null)
            {
                return;

            }
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                            .UseSqlite(_connectionString)
                            .Options;

            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {

                dbContext.Publishers.Add(model);
                dbContext.SaveChanges();
            }
        }
  
        public IEnumerable<Publisher> Select(Publisher model)
        {
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                           .UseSqlite(_connectionString)
                           .Options;
            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                if (model == null)
                {
                    return dbContext.Publishers.ToList();
                }
                else
                {
                    return dbContext.Publishers.Where(a => a.Name == model.Name).ToList();
                }
            }
        }

        public void Update(Publisher model)
        {
            if (model.Name == null && model.Contacts == null && model.Address == null)
            {
                return;

            }
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                           .UseSqlite(_connectionString)
                           .Options;
            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                var existingPublisher = dbContext.Publishers.FirstOrDefault(a => a.Id == model.Id);
                if (existingPublisher != null)
                {
                    if (model.Name != null)
                    {
                        existingPublisher.Name = model.Name;
                    }
                    if (model.Contacts != null)
                    {
                        existingPublisher.Contacts = model.Contacts;
                    }
                    if (model.Address != null)
                    {
                        existingPublisher.Address = model.Address;
                    }
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
