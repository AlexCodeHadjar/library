using library.Data.Models;
using library.ViewModels;
using library.Data.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using library.DataBase;
using System.Data.Entity;
using library.Controllers;

namespace library.Controllers
{
    ///<summary>
    ///��������� ��� ������ � ������������� Catalog 
    /// </summary>
    public class HomeController : Controller
    {
        static string connectionString = "Data Source=Catalogsdata.db";
        private DatabaseHelper _databaseHelper = new DatabaseHelper(connectionString);

        ///<summary>
        ///_author ����������� �������� ���������� ���������� IAuthor
        /// </summary>
        private readonly IAuthor _author;

        ///<summary>
        ///_publicsher ����������� �������� ���������� ���������� IPublicsher
        /// </summary>
        private readonly IPublicsher _publicsher;

        ///<summary>
        ///_bibliographicmaterial ����������� �������� ���������� ���������� IBibliographicmaterial
        /// </summary>
        private readonly IBibliographicmaterial _bibliographicmaterial;
        private readonly IUser _user;


        ///<summary>
        ///IAuthor iauthor �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
        ///IPublicsher ipublicsher �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
        /// IBibliographicmaterial ibibliographicmaterial �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
        /// </summary>
        public HomeController(IAuthor iauthor, IPublicsher ipublicsher, IBibliographicmaterial ibibliographicmaterial, IUser iuser)
        {

            _author = iauthor;
            _publicsher = ipublicsher;
            _bibliographicmaterial = ibibliographicmaterial;
            _user = iuser;

        }
        ///<summary>
        ///����� ������������� Catalog
        /// </summary>
        [HttpGet]
        public ViewResult Catalog()
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllAuthors = _author.AllAuthors;
            libraryobj.AllPublishers = _publicsher.AllPublicshers;
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.AllBibliographicmaterial;
            return View(libraryobj);

        }
        [HttpPost]
        public IActionResult Catalog(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.AllAuthors : _author.SelectAuthor(nameAuthor);
            libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.AllPublicshers : _publicsher.SelectPublisher(namePublisher);
            libraryobj.AllBibliographicmaterial = string.IsNullOrEmpty(nameBibliographicmaterial) ? _bibliographicmaterial.AllBibliographicmaterial : _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher);
           
            // libraryobj.AllUsers = _user.AllUsers;

            return View(libraryobj);

        }
        [HttpGet]
        public ViewResult CatalogAdmin()
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllAuthors = _author.AllAuthors;
            libraryobj.AllPublishers = _publicsher.AllPublicshers;
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.AllBibliographicmaterial;
            return View(libraryobj);

        }
        [HttpPost]
        public IActionResult CatalogAdmin(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.AllAuthors : _author.SelectAuthor(nameAuthor);
            libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.AllPublicshers : _publicsher.SelectPublisher(namePublisher);
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial,date,nameAuthor,namePublisher);
           

            return View(libraryobj);

        }
        [HttpPost]
        public ViewResult PageBibliographicmaterial(int materialId)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            IEnumerable<Bibliographicmaterial> allBibliographicmaterial = _bibliographicmaterial.AllBibliographicmaterial;


            libraryobj.AllBibliographicmaterial = allBibliographicmaterial.Where(a => a.Id == materialId);
            return View(libraryobj);
        }
        [HttpPost]
        public ViewResult PageBibliographicmaterialAdmin(int materialId)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            IEnumerable<Bibliographicmaterial> allBibliographicmaterial = _bibliographicmaterial.AllBibliographicmaterial;
            libraryobj.AllAuthors = _author.AllAuthors;
            libraryobj.AllPublishers = _publicsher.AllPublicshers;

            libraryobj.AllBibliographicmaterial = allBibliographicmaterial.Where(a => a.Id == materialId);
            return View(libraryobj);
        }
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedaction(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null)
        {
           string connectionString = "Data Source=Catalogsdata.db";






            // �������� ������ �����������
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
      
            Bibliographicmaterial newMaterial = new Bibliographicmaterial()
            {
                Name = nameBibliographicmaterial,
                Date = date,
                Img = "pict1.jpg",
                Author = new Author()
                {
                    Id = 1
                },
                Publisher = new Publisher()
                {
                    Id = 1
                }
            };

            databaseHelper.AddBibliographicMaterial(newMaterial);
            return RedirectToAction("CatalogAdmin", "Home");
            

        }
    }
}
