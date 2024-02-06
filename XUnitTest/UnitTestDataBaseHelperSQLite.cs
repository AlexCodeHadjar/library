using Microsoft.Extensions.DependencyInjection;
using Xunit;
using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;
using DataBaseHelperSQLite.DataBase.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Data.Entity;
using library;
using Microsoft.AspNetCore.Builder;
using XUnitTest;


namespace XUnitTestProject
{
    public class UnitTestDataBaseHelperSQLite
    {

     
       // public const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
       // public const string CONNECTION_STRING = "Data Source=C:\\Users\\user\\source\\repos\\library\\Catalogsdata.db";

        public const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
        private readonly MockedServiceCollection _serviceCollection = new MockedServiceCollection();


        private readonly IDataBaseHelperModels<Author> _authorServices;
        public UnitTestDataBaseHelperSQLite()
        {
            _authorServices = (IDataBaseHelperModels<Author>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<Author>));

            // _authorServices = new DataBaseAuthor(CONNECTION_STRING);



        }

        [Fact]
        public void TestDataBaseAuthorInsert()
        {
            
            Author author = new Author() { FullName = "Григорий", Contacts = "Contacts", Information = "Many-many"};
           

           
                var i = _authorServices.Select().Count();
            
                _authorServices.Insert(author);
               
                
               
                Assert.NotEqual(i,_authorServices.Select().Count());
            
        }
        
    }
}
