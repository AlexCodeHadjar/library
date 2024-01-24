using library.Data.Models;
using library.DataBase.Contract;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace library.DataBase.ImpI
{
    public class DataBaseBibliographicmaterial : DatabaseHelper, IDataBaseHelperModels<BibliographicMaterial>
    {
        public DataBaseBibliographicmaterial(string dbConnectionString) : base(dbConnectionString) { }

        public void Delete(int id)
        {
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                            .UseSqlite(_connectionString)
                            .Options;

            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                var bibliographicMaterial = dbContext.BibliographicMaterials.FirstOrDefault(a => a.Id == id);
                if (bibliographicMaterial != null)
                {


                    dbContext.BibliographicMaterials.Remove(bibliographicMaterial);
                    dbContext.SaveChanges();
                }
            }
        }

        public void Insert(BibliographicMaterial model)
        {
            if (model == null)
            {
                return;
            }

            if (model.PublisherId == null || model.AuthorId == null || model.Date == null || model.Name == null || model.Img == null)
            {
                return;

            }
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                            .UseSqlite(_connectionString)
                            .Options;

            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {

                dbContext.BibliographicMaterials.Add(model);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<BibliographicMaterial> Select(BibliographicMaterial model)
        {
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                         .UseSqlite(_connectionString)
                         .Options;
            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                if (model == null)
                {
                    return dbContext.BibliographicMaterials
                        .Include(b => b.Author)
                        .Include(b => b.Publisher)
                        .ToList();
                }
                else
                {
                    return dbContext.BibliographicMaterials
                        .Include(b => b.Author)
                        .Include(b => b.Publisher)
                        .Where(b => b.Name == model.Name)
                        .ToList();
                }
            }
        }

        public void Update(BibliographicMaterial model)
        {
            if (model.Name == null && model.Date == null && model.PublisherId == null && model.AuthorId == null && model.Img == null)
            {
                return;

            }
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                           .UseSqlite(_connectionString)
                           .Options;
            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                var existingBibliographicMaterial = dbContext.BibliographicMaterials.FirstOrDefault(a => a.Id == model.Id);
                if (existingBibliographicMaterial != null)
                {
                    if (model.Name != null)
                    {
                        existingBibliographicMaterial.Name = model.Name;
                    }
                    if (model.Date != null)
                    {
                        existingBibliographicMaterial.Date = model.Date;
                    }
                    if (model.PublisherId != null)
                    {
                        existingBibliographicMaterial.PublisherId = model.PublisherId;
                    }
                    if (model.AuthorId != null)
                    {
                        existingBibliographicMaterial.AuthorId = model.AuthorId;
                    }
                    if (model.Img != null)
                    {
                        existingBibliographicMaterial.Img = model.Img;
                    }
                    dbContext.SaveChanges();
                }
            }
        }

    }
}
