using Microsoft.Extensions.DependencyInjection;
using Xunit;
using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;
using DataBaseHelperSQLite.DataBase.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Data.Entity;
using library;


namespace XUnitTestProject
{
    public class UnitTestDataBaseHelperSQLite
    {

      // public const string CONNECTION_STRING = "Data Source=C:\\Users\\user\\source\\repos\\library\\library\\Catalogsdata.db";
        public const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
        public UnitTestDataBaseHelperSQLite()
        {
           
        }
        
        [Fact]
        public void TestDataBaseAuthorDelete()
        {
            var servicesCollections = new AllServices();
           var collections =  servicesCollections.Services();
       
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                         .UseSqlite(CONNECTION_STRING)
                         .Options;
            DataBaseAuthor dataBaseAuthor = new(CONNECTION_STRING);

            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                
                dataBaseAuthor.Delete(1);

                // Проверяем, что автор успешно удален из базы данных
                var deletedAuthor = dbContext.Authors.Find(1);
                Assert.Null(deletedAuthor);
            }
        }
        [Fact]
        public void TestDataBaseAuthorInsert()
        {
            Author author = new Author() { FullName = "Григорий", Contacts = "Contacts", Information = "Many-many" };
            var options = new DbContextOptionsBuilder<CUsersusersourcereposlibrarylibraryCatalogsdatadbContext>()
                            .UseSqlite(CONNECTION_STRING)
                            .Options;

            using (var dbContext = new CUsersusersourcereposlibrarylibraryCatalogsdatadbContext(options))
            {
                dbContext.Authors.Add(author);
                int recordsAffected = dbContext.SaveChanges();
                Assert.True(recordsAffected > 0);
            }
        }
        
    }
}
