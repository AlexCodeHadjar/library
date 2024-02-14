using DataBaseHelperSQLite.DataBase.Contract;
using library.Service.Contract;
using static library.Service.Impl.CatalogService;
using library.ViewModels;
using DataBaseHelperSQLite.Data.Models;

namespace XUnitTest.UnitTestService
{
    public class TestServiceCatalog
    {
        private readonly MockedServiceCollection _serviceCollection = new MockedServiceCollection();
        private readonly ICatalogService _libraryServices;
        private readonly IDataBaseHelperModels<BibliographicMaterial> _bibliographicmaterialServices;
        private readonly IDataBaseHelperModels<Author> _authorServices;
        private readonly IDataBaseHelperModels<Publisher> _publisherServices;

        public TestServiceCatalog()
        {
            _bibliographicmaterialServices = (IDataBaseHelperModels<BibliographicMaterial>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<BibliographicMaterial>));
            _authorServices = (IDataBaseHelperModels<Author>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<Author>));
            _publisherServices = (IDataBaseHelperModels<Publisher>)_serviceCollection.GetService(typeof(IDataBaseHelperModels<Publisher>));
            _libraryServices = _serviceCollection.GetRequiredService<ICatalogService>();

        }

        [Fact]
        public void Test_SortLibraryModels_SortByDate()
        {
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = null, date = null;
            SortBy sortBy = new SortBy() { SortDate = true, SortNameAuthor = false, SortNamePublisher = false, SortNameBibliographicmaterial = false };

            AllLibraryModels allLibraryModelsBefore = _libraryServices.StartLibraryModels();
            AllLibraryModels allLibraryModelsAfter = _libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy);
            Assert.NotEqual(allLibraryModelsBefore.AllBibliographicmaterial, allLibraryModelsAfter.AllBibliographicmaterial);


        }
        [Fact]
        public void Test_SortLibraryModels_SortByNameAuthor()
        {
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = null, date = null;
            SortBy sortBy = new SortBy() { SortDate = false, SortNameAuthor = true, SortNamePublisher = false, SortNameBibliographicmaterial = false };

            AllLibraryModels allLibraryModelsBefore = _libraryServices.StartLibraryModels();
            AllLibraryModels allLibraryModelsAfter = _libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy);
            Assert.NotEqual(allLibraryModelsBefore.AllBibliographicmaterial, allLibraryModelsAfter.AllBibliographicmaterial);


        }
        [Fact]
        public void Test_SortLibraryModels_SortByNamePublisher()
        {
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = null, date = null;
            SortBy sortBy = new SortBy() { SortDate = false, SortNameAuthor = false, SortNamePublisher = true, SortNameBibliographicmaterial = false };

            AllLibraryModels allLibraryModelsBefore = _libraryServices.StartLibraryModels();
            AllLibraryModels allLibraryModelsAfter = _libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy);
            Assert.NotEqual(allLibraryModelsBefore.AllBibliographicmaterial, allLibraryModelsAfter.AllBibliographicmaterial);


        }
        [Fact]
        public void Test_SortLibraryModels_SortByNameBibliographicmaterial()
        {
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = null, date = null;
            SortBy sortBy = new SortBy() { SortDate = false, SortNameAuthor = false, SortNamePublisher = false, SortNameBibliographicmaterial = true };

            AllLibraryModels allLibraryModelsBefore = _libraryServices.StartLibraryModels();
            AllLibraryModels allLibraryModelsAfter = _libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy);
            Assert.NotEqual(allLibraryModelsBefore.AllBibliographicmaterial, allLibraryModelsAfter.AllBibliographicmaterial);


        }
        [Fact]
        public void Test_SortLibraryModels_SearchBynameBibliographicmaterial()
        {
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 });
            string nameBibliographicmaterial = "Удалить", nameAuthor = null, namePublisher = null, date = null;
            SortBy sortBy = new SortBy() { };


            AllLibraryModels allLibraryModelsAfter = _libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy);
            bool checkTestValue = allLibraryModelsAfter.AllBibliographicmaterial.Count() == 1;
            _bibliographicmaterialServices.Delete(-100);
            Assert.True(checkTestValue);
        }
        [Fact]
        public void Test_SortLibraryModels_SearchBynameAuthor()
        {
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 });
            string nameBibliographicmaterial = null, nameAuthor = "H.R.R. Tolkien", namePublisher = null, date = null;
            SortBy sortBy = new SortBy() { };


            AllLibraryModels allLibraryModelsAfter = _libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy);
            var checkTestValue = allLibraryModelsAfter.AllAuthors.Count();
            _bibliographicmaterialServices.Delete(-100);
            Assert.True(checkTestValue == 1);
        }
        [Fact]
        public void Test_SortLibraryModels_SearchBynamePublisher()
        {
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 });
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = "Unwin", date = null;
            SortBy sortBy = new SortBy() { };


            AllLibraryModels allLibraryModelsAfter = _libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy);
            var checkTestValue = allLibraryModelsAfter.AllPublishers.Count();
            _bibliographicmaterialServices.Delete(-100);
            Assert.True(checkTestValue == 1);
        }

        [Fact]
        public void Test_SortLibraryModels_SearchByDate()
        {
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 });
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = null, date = "2068";
            SortBy sortBy = new SortBy() { };


            var allLibraryModelsAfter = _libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy);
            bool checkTestValue = allLibraryModelsAfter.AllBibliographicmaterial.Count() == 1;
            _bibliographicmaterialServices.Delete(-100);
            Assert.True(checkTestValue);
        }
        [Fact]
        public void Test_SortLibraryModels_SearchByAll()
        {
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 });
            string nameBibliographicmaterial = "Удалить", nameAuthor = "H.R.R. Tolkien", namePublisher = "Unwin", date = "2068";
            SortBy sortBy = new SortBy() { };


            var allLibraryModelsAfter = _libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy);
            bool checkTestValue = allLibraryModelsAfter.AllBibliographicmaterial.Count() == 1;
            _bibliographicmaterialServices.Delete(-100);
            Assert.True(checkTestValue);
        }
        [Fact]
        public void Test_StartLibraryModels_AllAuthors()
        {
            var testValueBefore = _libraryServices.StartLibraryModels().AllAuthors.Count();
            _authorServices.Insert(new Author { Id = -100, FullName = "Удалить", Contacts = " Удалить", Information = "Удалить" });
            var testValueAfter = _libraryServices.StartLibraryModels().AllAuthors.Count();
            _authorServices.Delete(-100);
            Assert.NotEqual(testValueBefore, testValueAfter);
        }
        [Fact]
        public void Test_StartLibraryModels_AllPublishers()
        {
            var testValueBefore = _libraryServices.StartLibraryModels().AllPublishers.Count();
            _publisherServices.Insert(new Publisher { Id = -100, Name = "Удалить", Contacts = "Удалить", Address = "Удалить" });
            var testValueAfter = _libraryServices.StartLibraryModels().AllPublishers.Count();
            _publisherServices.Delete(-100);
            Assert.NotEqual(testValueBefore, testValueAfter);
        }
        [Fact]
        public void Test_StartLibraryModels_AllBibliographicmaterial()
        {
            var testValueBefore = _libraryServices.StartLibraryModels().AllBibliographicmaterial.Count();
            _bibliographicmaterialServices.Insert(new BibliographicMaterial { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 });
            var testValueAfter = _libraryServices.StartLibraryModels().AllBibliographicmaterial.Count();
            _bibliographicmaterialServices.Delete(-100);
            Assert.NotEqual(testValueBefore, testValueAfter);
        }
        [Fact]
        public void Test_PageBibliographicmaterial()
        {
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 };
            _bibliographicmaterialServices.Insert(bibliographicMaterialTest);
            IEnumerable<BibliographicMaterial> bibliographicMaterialTestSearch = _libraryServices.PageBibliographicmaterial(-100).AllBibliographicmaterial;
            _bibliographicmaterialServices.Delete(-100);
            Assert.Equal(bibliographicMaterialTest.Id, bibliographicMaterialTestSearch.First().Id);
        }
        [Fact]
        public void Test_PageBibliographicmaterialAdmin()
        {
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 };
            _bibliographicmaterialServices.Insert(bibliographicMaterialTest);
            IEnumerable<BibliographicMaterial> bibliographicMaterialTestSearch = _libraryServices.PageBibliographicmaterial(-100).AllBibliographicmaterial;
            _bibliographicmaterialServices.Delete(-100);
            Assert.Equal(bibliographicMaterialTest.Id, bibliographicMaterialTestSearch.First().Id);
        }
        [Fact]
        public void Test_SelectBibliographicmaterial()
        {
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = null, date = null;
            var bibliographicmateriaValue = _libraryServices.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).Count();

            Assert.True(bibliographicmateriaValue > 0);

        }
        [Fact]
        public void Test_SelectBibliographicmaterial_ByNameBibliographicmaterial()
        {
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = null, date = null;
            var bibliographicmateriaValueBefore = _libraryServices.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).Count();
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 };
            _bibliographicmaterialServices.Insert(bibliographicMaterialTest);
            var bibliographicmateriaValueAfter = _libraryServices.SelectBibliographicmaterial("Удалить", date, nameAuthor, namePublisher).Count();
            _bibliographicmaterialServices.Delete(-100);
            Assert.NotEqual(bibliographicmateriaValueBefore, bibliographicmateriaValueAfter);
            Assert.True(bibliographicmateriaValueAfter == 1);
        }
        [Fact]
        public void Test_SelectBibliographicmaterial_ByDate()
        {
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = null, date = null;
            var bibliographicmateriaValueBefore = _libraryServices.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).Count();
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 };
            _bibliographicmaterialServices.Insert(bibliographicMaterialTest);
            var bibliographicmateriaValueAfter = _libraryServices.SelectBibliographicmaterial(nameBibliographicmaterial, "2068", nameAuthor, namePublisher).Count();
            _bibliographicmaterialServices.Delete(-100);
            Assert.NotEqual(bibliographicmateriaValueBefore, bibliographicmateriaValueAfter);
            Assert.True(bibliographicmateriaValueAfter == 1);
        }
        [Fact]
        public void Test_SelectBibliographicmaterial_ByNameAuthor()
        {
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = null, date = null;
            var bibliographicmateriaValueBefore = _libraryServices.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).Count();
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 };
            _bibliographicmaterialServices.Insert(bibliographicMaterialTest);
            var bibliographicmateriaValueAfter = _libraryServices.SelectBibliographicmaterial(nameBibliographicmaterial, date, "H.R.R. Tolkien", namePublisher).Count();
            _bibliographicmaterialServices.Delete(-100);
            Assert.NotEqual(bibliographicmateriaValueBefore, bibliographicmateriaValueAfter);
            Assert.True(bibliographicmateriaValueAfter == 3);
        }
        [Fact]
        public void Test_SelectBibliographicmaterial_ByNamePublisher()
        {
            string nameBibliographicmaterial = null, nameAuthor = null, namePublisher = null, date = null;
            var bibliographicmateriaValueBefore = _libraryServices.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).Count();
            BibliographicMaterial bibliographicMaterialTest = new BibliographicMaterial() { Id = -100, Name = "Удалить", Date = "2068", Img = "Удалить", AuthorId = 5, PublisherId = 2 };
            _bibliographicmaterialServices.Insert(bibliographicMaterialTest);
            var bibliographicmateriaValueAfter = _libraryServices.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, "Scribner").Count();
            _bibliographicmaterialServices.Delete(-100);
            Assert.NotEqual(bibliographicmateriaValueBefore, bibliographicmateriaValueAfter);
            Assert.True(bibliographicmateriaValueAfter == 3);
        }


    }
}