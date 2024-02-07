using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTest.UnitTestDataBaseHelperSQLite
{
    public class TestDataBasePublisher
    {
        public const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
        private readonly MockedServiceCollection _serviceCollection = new MockedServiceCollection();
        private readonly IDataBaseHelperModels<Publisher> _publisherServices;
        public TestDataBasePublisher()
        {
            _publisherServices = (IDataBaseHelperModels<Publisher>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<Publisher>));

        }
        [Fact]
        public void Test_Insert_Publisher()
        {

            Publisher Publisher = new Publisher() { Id = -101,  Name= "Название", Contacts = "Contacts", Address = "Where-Where" };

            var countStart = _publisherServices.Select().Count();

            _publisherServices.Insert(Publisher);


            var countEnd = _publisherServices.Select().Count();
            _publisherServices.Delete(-101);
            Assert.NotEqual(countStart, countEnd);


        }
        [Fact]
        public void Test_Insert_EmptyPublisher()
        {

            Publisher Publisher = new() { Id = -101 };



            var countStart = _publisherServices.Select().Count();

            _publisherServices.Insert(Publisher);
            var countEnd = _publisherServices.Select().Count();
            _publisherServices.Delete(-101);



            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyName_Publisher()
        {

            Publisher Publisher = new Publisher() { Id = -101, Name = null, Contacts = "Contacts", Address = "Where-where" };



            var countStart = _publisherServices.Select().Count();

            _publisherServices.Insert(Publisher);


            var countEnd = _publisherServices.Select().Count();
            _publisherServices.Delete(-101);
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyContacts_Publisher()
        {

            Publisher Publisher = new Publisher() { Id = -101, Name = "Название", Contacts = null, Address = "Where-where" };



            var countStart = _publisherServices.Select().Count();

            _publisherServices.Insert(Publisher);


            var countEnd = _publisherServices.Select().Count();
            _publisherServices.Delete(-101);
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyAddress_Publisher()
        {

            Publisher Publisher = new Publisher() { Id = -101, Name = "Название", Contacts = "Contacts", Address = null };



            var countStart = _publisherServices.Select().Count();

            _publisherServices.Insert(Publisher);


            var countEnd = _publisherServices.Select().Count();
            _publisherServices.Delete(-101);
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Update_Publisher()
        {
            _publisherServices.Insert(new Publisher { Id = -100, Name = "Удалить", Contacts = "Удалить", Address = "Удалить" });
            Publisher Publisher = new Publisher { Id = -100 };

            Publisher PublisherSearch = _publisherServices.Select(Publisher).First();
            Publisher PublisherTest = new Publisher { Id = -100, Name = "новое Название", Contacts = "новый Contacts", Address = " новый Where-where" };
            _publisherServices.Update(PublisherTest);
            Publisher updatedPublisher = _publisherServices.Select(PublisherSearch).First();
            _publisherServices.Delete(-100);
            Assert.NotEqual(updatedPublisher.Name, PublisherSearch.Name);
            Assert.NotEqual(updatedPublisher.Contacts, PublisherSearch.Contacts);
            Assert.NotEqual(updatedPublisher.Address, PublisherSearch.Address);

        }
        [Fact]
        public void Test_Update_EmptyName_Publisher()
        {

            _publisherServices.Insert(new Publisher { Id = -100, Name = "Удалить", Contacts = "Удалить", Address = "Удалить" });
            Publisher Publisher = new Publisher { Id = -100 };



            Publisher PublisherSearch = _publisherServices.Select(Publisher).First();
            Publisher PublisherTest = new Publisher { Id = -100, Name = null, Contacts = "iСontacts", Address = "Информация об издательстве" };
            _publisherServices.Update(PublisherTest);
            Publisher updatedPublisher = _publisherServices.Select(PublisherSearch).First();
            _publisherServices.Delete(-100);
            Assert.Equal(updatedPublisher.Name, PublisherSearch.Name);
            Assert.NotEqual(updatedPublisher.Contacts, PublisherSearch.Contacts);
            Assert.NotEqual(updatedPublisher.Address, PublisherSearch.Address);



        }
        [Fact]
        public void Test_Update_EmptyContacts_Publisher()
        {
            _publisherServices.Insert(new Publisher { Id = -100, Name = "Удалить", Contacts = "Удалить", Address = "Удалить" });
            Publisher Publisher = new Publisher { Id = -100 };

            Publisher PublisherSearch = _publisherServices.Select(Publisher).First();
            Publisher PublisherTest = new Publisher { Id = -100, Name = "новое Название", Contacts = null, Address = "Информация об издательстве" };
            _publisherServices.Update(PublisherTest);
            Publisher updatedPublisher = _publisherServices.Select(PublisherSearch).First();
            _publisherServices.Delete(-100);
            Assert.NotEqual(updatedPublisher.Name, PublisherSearch.Name);
            Assert.Equal(updatedPublisher.Contacts, PublisherSearch.Contacts);
            Assert.NotEqual(updatedPublisher.Address, PublisherSearch.Address);

        }
        [Fact]
        public void Test_Update_EmptyAddress_Publisher()
        {
            _publisherServices.Insert(new Publisher { Id = -100, Name = "Удалить", Contacts = "Удалить", Address = "Удалить" });
            Publisher Publisher = new Publisher { Id = -100 };
            Publisher PublisherSearch = _publisherServices.Select(Publisher).First();
            Publisher PublisherTest = new Publisher { Id = -100, Name = "новое Название", Contacts = "Сontacts", Address = null };
            _publisherServices.Update(PublisherTest);
            Publisher updatedPublisher = _publisherServices.Select(PublisherSearch).First();
            _publisherServices.Delete(-100);
            Assert.NotEqual(updatedPublisher.Name, PublisherSearch.Name);
            Assert.NotEqual(updatedPublisher.Contacts, PublisherSearch.Contacts);
            Assert.Equal(updatedPublisher.Address, PublisherSearch.Address);

        }
        [Fact]
        public void Test_Update_Empty_Publisher()
        {
            _publisherServices.Insert(new Publisher { Id = -100, Name = "Удалить", Contacts = "Удалить", Address = "Удалить" });
            Publisher Publisher = new Publisher { Id = -100 };

            Publisher PublisherSearch = _publisherServices.Select(Publisher).First();
            Publisher PublisherTest = new Publisher { Id = -100, Name = null, Contacts = null, Address = null };
            _publisherServices.Update(PublisherTest);
            Publisher updatedPublisher = _publisherServices.Select(PublisherSearch).First();
            _publisherServices.Delete(-100);
            Assert.Equal(updatedPublisher.Name, PublisherSearch.Name);
            Assert.Equal(updatedPublisher.Contacts, PublisherSearch.Contacts);
            Assert.Equal(updatedPublisher.Address, PublisherSearch.Address);

        }
        [Fact]
        public void Test_Delete_Publisher()
        {
            _publisherServices.Insert(new Publisher { Id = -100, Name = "Удалить", Contacts = " Удалить", Address = "Удалить" });
            Publisher Publisher = new Publisher { Id = -100 };
            Publisher PublisherSearch = _publisherServices.Select(Publisher).First();
            _publisherServices.Delete(-100);
            PublisherSearch = _publisherServices.Select(Publisher).FirstOrDefault();
            Assert.True(PublisherSearch == null);

        }
        [Fact]
        public void Test_Select_Publisher()
        {
            var startValue = _publisherServices.Select().Count();
            _publisherServices.Insert(new Publisher { Id = -100, Name = "Удалить", Contacts = " Удалить", Address = "Удалить" });
            var afterValue = _publisherServices.Select().Count();
            _publisherServices.Delete(-100);
            Assert.True(startValue + 1 == afterValue);
        }
        [Fact]
        public void Test_SelectPublisher_Publisher()
        {
            _publisherServices.Insert(new Publisher { Id = -100, Name = "Удалить", Contacts = " Удалить", Address = "Удалить" });
            Publisher PublisherExpectation = new Publisher { Id = -100, Name = "Удалить", Contacts = " Удалить", Address = "Удалить" };
            Publisher seachPublisher = new Publisher() { Id = -100 };
            Publisher afterValue = _publisherServices.Select(seachPublisher).First();
            _publisherServices.Delete(-100);
            Assert.True(PublisherExpectation.Id == afterValue.Id);
        }
    }
}
