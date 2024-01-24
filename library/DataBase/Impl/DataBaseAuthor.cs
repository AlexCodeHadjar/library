using library.Data.Models;
using library.DataBase.Contract;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace library.DataBase.Impl
{
    public class DataBaseAuthor : DatabaseHelper, IDataBaseHelperModels<Author>
    {
        public DataBaseAuthor(string dbConnectionString) : base(dbConnectionString) { }
        public void Delete(int idAuthor)
        {
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                            .UseSqlite(_connectionString)
                            .Options;

            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                var author = dbContext.Authors.FirstOrDefault(a => a.Id == idAuthor);
                if (author != null)
                {
                    if (author.BibliographicMaterials.Any())
                    {
                      
                        return;
                    }

                    dbContext.Authors.Remove(author);
                    dbContext.SaveChanges();
                }
            }
        }
        public void Insert(Author author)
        {
            if (author.FullName == null || author.Contacts == null || author.Information == null)
            {
                return;
            }
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                            .UseSqlite(_connectionString)
                            .Options;

            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {

                dbContext.Authors.Add(author);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Author> Select(Author model)
        {
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                           .UseSqlite(_connectionString)
                           .Options;
            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                if (model == null)
                {
                    return dbContext.Authors.ToList();
                }
                else
                {
                    return dbContext.Authors.Where(a => a.FullName == model.FullName).ToList();
                }
            }
        }
 
        public void Update(Author author)
        {
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                           .UseSqlite(_connectionString)
                           .Options;
            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                var existingAuthor = dbContext.Authors.FirstOrDefault(a => a.Id == author.Id);
                if (existingAuthor != null)
                {
                    if (author.FullName != null)
                    {
                        existingAuthor.FullName = author.FullName;
                    }
                    if (author.Contacts != null)
                    {
                        existingAuthor.Contacts = author.Contacts;
                    }
                    if (author.Information != null)
                    {
                        existingAuthor.Information = author.Information;
                    }
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
