using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;

namespace XUnitTest.UnitTestDataBaseHelperSQLite
{

    public class TestDataBaseAuthor
    {
        public const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
        private readonly MockedServiceCollection _serviceCollection = new MockedServiceCollection();
        private readonly IDataBaseHelperModels<Author> _authorServices;
        public TestDataBaseAuthor()
        {
            _authorServices = (IDataBaseHelperModels<Author>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<Author>));

        }
        [Fact]
        public void Test_Insert_Author()
        {

            Author author = new Author() { Id = -101, FullName = "Григорий", Contacts = "Contacts", Information = "Many-many" };

            var countStart = _authorServices.Select().Count();

            _authorServices.Insert(author);


            var countEnd = _authorServices.Select().Count();
            _authorServices.Delete(-101);
            Assert.NotEqual(countStart, countEnd);


        }
        [Fact]
        public void Test_Insert_EmptyAuthor()
        {

            Author author = new() { Id = -101 };



            var countStart = _authorServices.Select().Count();

            _authorServices.Insert(author);
            var countEnd = _authorServices.Select().Count();
            _authorServices.Delete(-101);



            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyFullName_Author()
        {

            Author author = new Author() { Id = -101, FullName = null, Contacts = "Contacts", Information = "Many-many" };



            var countStart = _authorServices.Select().Count();

            _authorServices.Insert(author);


            var countEnd = _authorServices.Select().Count();
            _authorServices.Delete(-101);
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyContacts_Author()
        {

            Author author = new Author() { Id = -101, FullName = "Григорий", Contacts = null, Information = "Many-many" };



            var countStart = _authorServices.Select().Count();

            _authorServices.Insert(author);


            var countEnd = _authorServices.Select().Count();
            _authorServices.Delete(-101);
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyInformation_Author()
        {

            Author author = new Author() { Id = -101, FullName = "Григорий", Contacts = "Contacts", Information = null };



            var countStart = _authorServices.Select().Count();

            _authorServices.Insert(author);


            var countEnd = _authorServices.Select().Count();
            _authorServices.Delete(-101);
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Update_Author()
        {
            _authorServices.Insert(new Author { Id = -100, FullName = "Удалить", Contacts = "Удалить", Information = "Удалить" });
            Author author = new Author { Id = -100 };

            Author authorSearch = _authorServices.Select(author).First();
            Author authorTest = new Author { Id = -100, FullName = "Сергей Патрешов", Contacts = "новая ivan@example.com", Information = " новая Информация об авторе" };
            _authorServices.Update(authorTest);
            Author updatedAuthor = _authorServices.Select(authorSearch).First();
            _authorServices.Delete(-100);
            Assert.NotEqual(updatedAuthor.FullName, authorSearch.FullName);
            Assert.NotEqual(updatedAuthor.Contacts, authorSearch.Contacts);
            Assert.NotEqual(updatedAuthor.Information, authorSearch.Information);

        }
        [Fact]
        public void Test_Update_EmptyFullName_Author()
        {

            _authorServices.Insert(new Author { Id = -100, FullName = "Удалить", Contacts = "Удалить", Information = "Удалить" });
            Author author = new Author { Id = -100 };



            Author authorSearch = _authorServices.Select(author).First();
            Author authorTest = new Author { Id = -100, FullName = null, Contacts = "ivan@example.com", Information = "Информация об авторе" };
            _authorServices.Update(authorTest);
            Author updatedAuthor = _authorServices.Select(authorSearch).First();
            _authorServices.Delete(-100);
            Assert.Equal(updatedAuthor.FullName, authorSearch.FullName);
            Assert.NotEqual(updatedAuthor.Contacts, authorSearch.Contacts);
            Assert.NotEqual(updatedAuthor.Information, authorSearch.Information);



        }
        [Fact]
        public void Test_Update_EmptyContacts_Author()
        {
            _authorServices.Insert(new Author { Id = -100, FullName = "Удалить", Contacts = "Удалить", Information = "Удалить" });
            Author author = new Author { Id = -100 };

            Author authorSearch = _authorServices.Select(author).First();
            Author authorTest = new Author { Id = -100, FullName = "Сергей Патрешов", Contacts = null, Information = "Информация об авторе" };
            _authorServices.Update(authorTest);
            Author updatedAuthor = _authorServices.Select(authorSearch).First();
            _authorServices.Delete(-100);
            Assert.NotEqual(updatedAuthor.FullName, authorSearch.FullName);
            Assert.Equal(updatedAuthor.Contacts, authorSearch.Contacts);
            Assert.NotEqual(updatedAuthor.Information, authorSearch.Information);

        }
        [Fact]
        public void Test_Update_EmptyInformation_Author()
        {
            _authorServices.Insert(new Author { Id = -100, FullName = "Удалить", Contacts = "Удалить", Information = "Удалить" });
            Author author = new Author { Id = -100 };
            Author authorSearch = _authorServices.Select(author).First();
            Author authorTest = new Author { Id = -100, FullName = "Сергей Патрешов", Contacts = "ivan@example.com", Information = null };
            _authorServices.Update(authorTest);
            Author updatedAuthor = _authorServices.Select(authorSearch).First();
            _authorServices.Delete(-100);
            Assert.NotEqual(updatedAuthor.FullName, authorSearch.FullName);
            Assert.NotEqual(updatedAuthor.Contacts, authorSearch.Contacts);
            Assert.Equal(updatedAuthor.Information, authorSearch.Information);

        }
        [Fact]
        public void Test_Update_Empty_Author()
        {
            _authorServices.Insert(new Author { Id = -100, FullName = "Удалить", Contacts = "Удалить", Information = "Удалить" });
            Author author = new Author { Id = -100 };

            Author authorSearch = _authorServices.Select(author).First();
            Author authorTest = new Author { Id = -100, FullName = null, Contacts = null, Information = null };
            _authorServices.Update(authorTest);
            Author updatedAuthor = _authorServices.Select(authorSearch).First();
            _authorServices.Delete(-100);
            Assert.Equal(updatedAuthor.FullName, authorSearch.FullName);
            Assert.Equal(updatedAuthor.Contacts, authorSearch.Contacts);
            Assert.Equal(updatedAuthor.Information, authorSearch.Information);

        }
        [Fact]
        public void Test_Delete_Author()
        {
            _authorServices.Insert(new Author { Id = -100, FullName = "Удалить", Contacts = " Удалить", Information = "Удалить" });
            Author author = new Author { Id = -100 };
            Author authorSearch = _authorServices.Select(author).First();
            _authorServices.Delete(-100);
            authorSearch = _authorServices.Select(author).FirstOrDefault();
            Assert.True(authorSearch == null);

        }
        [Fact]
        public void Test_Select_Author()
        {
            var startValue = _authorServices.Select().Count();
            _authorServices.Insert(new Author { Id = -100, FullName = "Удалить", Contacts = " Удалить", Information = "Удалить" });
            var afterValue = _authorServices.Select().Count();
            _authorServices.Delete(-100);
            Assert.True(startValue + 1 == afterValue);
        }
        [Fact]
        public void Test_SelectAuthor_Author()
        {
            _authorServices.Insert(new Author { Id = -100, FullName = "Удалить", Contacts = " Удалить", Information = "Удалить" });
            Author authorExpectation = new Author { Id = -100, FullName = "Удалить", Contacts = " Удалить", Information = "Удалить" };
            Author seachAuthor = new Author() { Id = -100 };
            Author afterValue = _authorServices.Select(seachAuthor).First();
            _authorServices.Delete(-100);
            Assert.True(authorExpectation.Id == afterValue.Id);
        }

    }
}
