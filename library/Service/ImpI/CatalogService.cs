using library.ViewModels;
using static library.DataBase.ImpI.DatabaseHelper;
using library.Data.Models;
using library.Service.Contract;
using library.DataBase.Contract;
using library.DataBase.ImpI;

namespace library.Service.ImpI
{

    public class CatalogService : ICatalogService
    {

        public class SortBy
        {
            public bool SortNameBibliographicmaterial { get; set; }
            public bool SortNameAuthor { get; set; }
            public bool SortNamePublisher { get; set; }
            public bool SortDate { get; set; }
        }

        private readonly IServiceProvider _serviceProvider;

        public CatalogService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public AllLibraryModels StartLibraryModels()
        {
            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllAuthors = _serviceProvider.GetRequiredService<IDataBaseHelperModels<Author>>().Select().OrderBy(a => a.FullName);
            libraryobj.AllPublishers = _serviceProvider.GetRequiredService<IDataBaseHelperModels<Publisher>>().Select().OrderBy(a => a.Name);
            libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperModels<BibliographicMaterial>>().Select().OrderBy(a => a.Name);
            libraryobj.AllImgs = _serviceProvider.GetRequiredService<IDataBaseHelperModels<BibliographicMaterial>>().Select().ToList();
            return libraryobj;
        }
        public AllLibraryModels PageBibliographicmaterial(int materialId)
        {
            AllLibraryModels libraryobj = new AllLibraryModels();

            libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperModels<BibliographicMaterial>>().Select().Where(a => a.Id == materialId);
            return libraryobj;
        }
        public AllLibraryModels PageBibliographicmaterialAdmin(int materialId)
        {
            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj = _serviceProvider.GetRequiredService<ICatalogService>().StartLibraryModels();
            libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperModels<BibliographicMaterial>>().Select().Where(a => a.Id == materialId);
            libraryobj.AllImgs = _serviceProvider.GetRequiredService<IDataBaseHelperModels<BibliographicMaterial>>().Select().ToList();
            return libraryobj;
        }
        public IEnumerable<BibliographicMaterial> SelectBibliographicmaterial(string nameBibliographicmaterial, string date, string nameAuthor, string namePublisher)
        {
            DatabaseHelper databaseHelper = new(null);
            Author dopauthor = new();
            dopauthor.FullName = nameAuthor;
            Publisher dopPublisher = new();
            dopPublisher.Name = namePublisher;
            DataBaseAuthor author = new(databaseHelper._connectionString);
            DataBasePublisher publisher = new(databaseHelper._connectionString);
            DataBaseBibliographicmaterial bibliographicmaterial = new(databaseHelper._connectionString);

            IEnumerable<BibliographicMaterial> filteredBibliographicmaterial = bibliographicmaterial.Select(null);

            if (!string.IsNullOrEmpty(nameBibliographicmaterial))
            {
                filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => a.Name == nameBibliographicmaterial);
            }

            if (!string.IsNullOrEmpty(date))
            {
                filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => int.Parse(a.Date) == int.Parse(date));
            }

            if (!string.IsNullOrEmpty(dopauthor.FullName))
            {
                var authors = author.Select(dopauthor).Select(a => a.Id);
                filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => authors.Contains(a.Author.Id));
            }

            if (!string.IsNullOrEmpty(dopPublisher.Name))
            {
                var publishers = publisher.Select(dopPublisher).Select(a => a.Id);
                filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => publishers.Contains(a.Publisher.Id));
            }

            return filteredBibliographicmaterial;
        }

        public AllLibraryModels SortLibraryModels(string nameBibliographicmaterial, string nameAuthor, string namePublisher, string date, SortBy sortBy)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();

            libraryobj.AllBibliographicmaterial = SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher);
            if (sortBy.SortNameAuthor)
            {

                libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<ICatalogService>().SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Author.FullName);
            }


            if (sortBy.SortNamePublisher)
            {
                libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<ICatalogService>().SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Publisher.Name);
            }

            if (sortBy.SortNameBibliographicmaterial)
            {
                libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<ICatalogService>().SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Name);
            }

            if (sortBy.SortDate)
            {
                libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<ICatalogService>().SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderByDescending(a => a.Date);
            }

            Author author = new();
            Publisher publisher = new();
            author.FullName = nameAuthor;
            publisher.Name = namePublisher;
            libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _serviceProvider.GetRequiredService<IDataBaseHelperModels<Publisher>>().Select().OrderBy(a => a.Name) : _serviceProvider.GetRequiredService<IDataBaseHelperModels<Publisher>>().Select(publisher).OrderBy(a => a.Name);
            libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _serviceProvider.GetRequiredService<IDataBaseHelperModels<Author>>().Select().OrderBy(a => a.FullName) : _serviceProvider.GetRequiredService<IDataBaseHelperModels<Author>>().Select(author).OrderBy(a => a.FullName);
            return libraryobj;
        }

    }
}


