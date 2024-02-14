using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;

namespace XUnitTest.UnitTestDataBaseHelperSQLite
{
    public class TestDataBaseBibliographicmaterial
    {

        public const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
        private readonly MockedServiceCollection _serviceCollection = new MockedServiceCollection();
        private readonly IDataBaseHelperModels<BibliographicMaterial> _bibliographicmaterialServices;
        public TestDataBaseBibliographicmaterial()
        {
            _bibliographicmaterialServices = (IDataBaseHelperModels<BibliographicMaterial>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<BibliographicMaterial>));

        }
   
        [Fact]
        public void Test_Insert_BibliographicMaterial()
        {

            BibliographicMaterial BibliographicMaterial = new BibliographicMaterial() { Id = -101, Name = "Григорий", Date = "2020",  Img= "pict3.jpg", AuthorId = 5,PublisherId = 2 };

            var countStart = _bibliographicmaterialServices.Select().Count();

            _bibliographicmaterialServices.Insert(BibliographicMaterial);


            var countEnd = _bibliographicmaterialServices.Select().Count();
            _bibliographicmaterialServices.Delete(-101);
            Assert.NotEqual(countStart, countEnd);


        }
        [Fact]
        public void Test_Insert_EmptyBibliographicMaterial()
        {

            BibliographicMaterial BibliographicMaterial = new() { Id = -101 };



            var countStart = _bibliographicmaterialServices.Select().Count();

            _bibliographicmaterialServices.Insert(BibliographicMaterial);
            var countEnd = _bibliographicmaterialServices.Select().Count();
            _bibliographicmaterialServices.Delete(-101);



            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyName_BibliographicMaterial()
        {

              BibliographicMaterial BibliographicMaterial = new BibliographicMaterial() { Id = -101, Name = null, Date = "2020",  Img= "pict3.jpg", AuthorId = 5,PublisherId = 2 };
          


            var countStart = _bibliographicmaterialServices.Select().Count();

            _bibliographicmaterialServices.Insert(BibliographicMaterial);


            var countEnd = _bibliographicmaterialServices.Select().Count();
            _bibliographicmaterialServices.Delete(-101);
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyDate_BibliographicMaterial()
        {


            BibliographicMaterial BibliographicMaterial = new BibliographicMaterial() { Id = -101, Name = null, Date = "2020", Img = "pict3.jpg", AuthorId = 5, PublisherId = 2 };


            var countStart = _bibliographicmaterialServices.Select().Count();

            _bibliographicmaterialServices.Insert(BibliographicMaterial);


            var countEnd = _bibliographicmaterialServices.Select().Count();
            _bibliographicmaterialServices.Delete(-101);
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Insert_EmptyImg_BibliographicMaterial()
        {

           
            BibliographicMaterial BibliographicMaterial = new BibliographicMaterial() { Id = -101, Name = "Григорий", Date = "2020", Img = null, AuthorId = 5, PublisherId = 2 };


            var countStart = _bibliographicmaterialServices.Select().Count();

            _bibliographicmaterialServices.Insert(BibliographicMaterial);


            var countEnd = _bibliographicmaterialServices.Select().Count();
            _bibliographicmaterialServices.Delete(-101);
            Assert.Equal(countStart, countEnd);

        }
        [Fact]
        public void Test_Update_BibliographicMaterial()
        {
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "Удалить", Img = "Удалить", AuthorId = 5, PublisherId = 2 });

            BibliographicMaterial bibliographicMaterial = new BibliographicMaterial { Id = -100 };

            BibliographicMaterial bibliographicMaterialSearch = _bibliographicmaterialServices.Select(bibliographicMaterial).First();
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = "новый Григорий", Date = "новый 2020", Img = "pict3.jpg", AuthorId = 1, PublisherId = 66 };
            _bibliographicmaterialServices.Update(bibliographicMaterialTest);
            BibliographicMaterial updatedBibliographicMaterial = _bibliographicmaterialServices.Select(bibliographicMaterialSearch).First();
            _bibliographicmaterialServices.Delete(-100);
            Assert.NotEqual(updatedBibliographicMaterial.Name, bibliographicMaterialSearch.Name);
            Assert.NotEqual(updatedBibliographicMaterial.Date, bibliographicMaterialSearch.Date);
            Assert.NotEqual(updatedBibliographicMaterial.Img, bibliographicMaterialSearch.Img);
            Assert.NotEqual(updatedBibliographicMaterial.AuthorId, bibliographicMaterialSearch.AuthorId);
            Assert.NotEqual(updatedBibliographicMaterial.PublisherId, bibliographicMaterialSearch.PublisherId);

        }
        
        [Fact]
        public void Test_Update_EmptyName_BibliographicMaterial()
        {

            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "Удалить", Img = "Удалить", AuthorId = 5, PublisherId = 2 });

            BibliographicMaterial bibliographicMaterial = new BibliographicMaterial { Id = -100 };

            BibliographicMaterial bibliographicMaterialSearch = _bibliographicmaterialServices.Select(bibliographicMaterial).First();
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = null, Date = "новый 2020", Img = "pict3.jpg", AuthorId = 1, PublisherId = 66 };
            _bibliographicmaterialServices.Update(bibliographicMaterialTest);
            BibliographicMaterial updatedBibliographicMaterial = _bibliographicmaterialServices.Select(bibliographicMaterialSearch).First();
            _bibliographicmaterialServices.Delete(-100);
            Assert.Equal(updatedBibliographicMaterial.Name, bibliographicMaterialSearch.Name);
            Assert.NotEqual(updatedBibliographicMaterial.Date, bibliographicMaterialSearch.Date);
            Assert.NotEqual(updatedBibliographicMaterial.Img, bibliographicMaterialSearch.Img);
            Assert.NotEqual(updatedBibliographicMaterial.AuthorId, bibliographicMaterialSearch.AuthorId);
            Assert.NotEqual(updatedBibliographicMaterial.PublisherId, bibliographicMaterialSearch.PublisherId);
           



        }
        [Fact]
        public void Test_Update_EmptyDate_BibliographicMaterial()
        {
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "Удалить", Img = "Удалить", AuthorId = 5, PublisherId = 2 });

            BibliographicMaterial bibliographicMaterial = new BibliographicMaterial { Id = -100 };

            BibliographicMaterial bibliographicMaterialSearch = _bibliographicmaterialServices.Select(bibliographicMaterial).First();
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = "новый Григорий", Date = null, Img = "pict3.jpg", AuthorId = 1, PublisherId = 66 };
            _bibliographicmaterialServices.Update(bibliographicMaterialTest);
            BibliographicMaterial updatedBibliographicMaterial = _bibliographicmaterialServices.Select(bibliographicMaterialSearch).First();
            _bibliographicmaterialServices.Delete(-100);
            Assert.NotEqual(updatedBibliographicMaterial.Name, bibliographicMaterialSearch.Name);
            Assert.Equal(updatedBibliographicMaterial.Date, bibliographicMaterialSearch.Date);
            Assert.NotEqual(updatedBibliographicMaterial.Img, bibliographicMaterialSearch.Img);
            Assert.NotEqual(updatedBibliographicMaterial.AuthorId, bibliographicMaterialSearch.AuthorId);
            Assert.NotEqual(updatedBibliographicMaterial.PublisherId, bibliographicMaterialSearch.PublisherId);

        }
        [Fact]
        public void Test_Update_EmptyImg_BibliographicMaterial()
        {
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "Удалить", Img = "Удалить", AuthorId = 5, PublisherId = 2 });

            BibliographicMaterial bibliographicMaterial = new BibliographicMaterial { Id = -100 };

            BibliographicMaterial bibliographicMaterialSearch = _bibliographicmaterialServices.Select(bibliographicMaterial).First();
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = "новый Григорий", Date = "новый 2020", Img = null, AuthorId = 1, PublisherId = 66 };
            _bibliographicmaterialServices.Update(bibliographicMaterialTest);
            BibliographicMaterial updatedBibliographicMaterial = _bibliographicmaterialServices.Select(bibliographicMaterialSearch).First();
            _bibliographicmaterialServices.Delete(-100);
            Assert.NotEqual(updatedBibliographicMaterial.Name, bibliographicMaterialSearch.Name);
            Assert.NotEqual(updatedBibliographicMaterial.Date, bibliographicMaterialSearch.Date);
            Assert.Equal(updatedBibliographicMaterial.Img, bibliographicMaterialSearch.Img);
            Assert.NotEqual(updatedBibliographicMaterial.AuthorId, bibliographicMaterialSearch.AuthorId);
            Assert.NotEqual(updatedBibliographicMaterial.PublisherId, bibliographicMaterialSearch.PublisherId);

        }
        [Fact]
        public void Test_Update_Empty_BibliographicMaterial()
        {
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "Удалить", Img = "Удалить", AuthorId = 5, PublisherId = 2 });

            BibliographicMaterial bibliographicMaterial = new BibliographicMaterial { Id = -100 };

            BibliographicMaterial bibliographicMaterialSearch = _bibliographicmaterialServices.Select(bibliographicMaterial).First();
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = null, Date = null, Img = null, AuthorId =null, PublisherId = null };
            _bibliographicmaterialServices.Update(bibliographicMaterialTest);
            BibliographicMaterial updatedBibliographicMaterial = _bibliographicmaterialServices.Select(bibliographicMaterialSearch).First();
            _bibliographicmaterialServices.Delete(-100);
            Assert.Equal(updatedBibliographicMaterial.Name, bibliographicMaterialSearch.Name);
            Assert.Equal(updatedBibliographicMaterial.Date, bibliographicMaterialSearch.Date);
            Assert.Equal(updatedBibliographicMaterial.Img, bibliographicMaterialSearch.Img);
            Assert.Equal(updatedBibliographicMaterial.AuthorId, bibliographicMaterialSearch.AuthorId);
            Assert.Equal(updatedBibliographicMaterial.PublisherId, bibliographicMaterialSearch.PublisherId);

        }
        
        [Fact]
        public void Test_Delete_BibliographicMaterial()
        {

            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "Удалить", Img = "Удалить", AuthorId = 5, PublisherId = 2 });

            BibliographicMaterial bibliographicMaterial = new BibliographicMaterial { Id = -100 };

            BibliographicMaterial bibliographicMaterialSearch = _bibliographicmaterialServices.Select(bibliographicMaterial).First();
            _bibliographicmaterialServices.Delete(-100);
            bibliographicMaterialSearch = _bibliographicmaterialServices.Select(bibliographicMaterial).FirstOrDefault();
            Assert.True(bibliographicMaterialSearch == null);

        }
        [Fact]
        public void Test_Select_BibliographicMaterial()
        {
            var startValue = _bibliographicmaterialServices.Select().Count();
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "Удалить", Img = "Удалить", AuthorId = 5, PublisherId = 2 });
            var afterValue = _bibliographicmaterialServices.Select().Count();
            _bibliographicmaterialServices.Delete(-100);
            Assert.True(startValue + 1 == afterValue);
        }
        [Fact]
        public void Test_SelectAuthor_BibliographicMaterial()
        {
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "Удалить", Img = "Удалить", AuthorId = 5, PublisherId = 2 });
            BibliographicMaterial bibliographicMaterialExpectation = new BibliographicMaterial (){ Id = -100, Name = "Удалить", Date = "Удалить", Img = "Удалить", AuthorId = 5, PublisherId = 2 };
            BibliographicMaterial seachBibliographicMaterial = new BibliographicMaterial() { Id = -100 };
            BibliographicMaterial afterValue = _bibliographicmaterialServices.Select(seachBibliographicMaterial).First();
            _bibliographicmaterialServices.Delete(-100);
            Assert.True(bibliographicMaterialExpectation.Id == afterValue.Id);
        }
        
    }
        
}
