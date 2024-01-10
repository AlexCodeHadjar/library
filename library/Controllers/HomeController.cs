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
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.IO;

namespace library.Controllers
{
    ///<summary>
    ///контролер для работы с предстваление Catalog 
    /// </summary>
    public class HomeController : Controller
    {
        static string connectionString = "Data Source=Catalogsdata.db";
        private DatabaseHelper _databaseHelper = new DatabaseHelper(connectionString);

        ///<summary>
        ///_author переменнная принимет реализацию интерфейса IAuthor
        /// </summary>
        private readonly IAuthor _author;

        ///<summary>
        ///_publicsher переменнная принимет реализацию интерфейса IPublicsher
        /// </summary>
        private readonly IPublicsher _publicsher;

        ///<summary>
        ///_bibliographicmaterial переменнная принимет реализацию интерфейса IBibliographicmaterial
        /// </summary>
        private readonly IBibliographicmaterial _bibliographicmaterial;
        private readonly IUser _user;
        private readonly IWebHostEnvironment _appEnvironment;


        ///<summary>
        ///IAuthor iauthor передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
        ///IPublicsher ipublicsher передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
        /// IBibliographicmaterial ibibliographicmaterial передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
        /// </summary>
        public HomeController(IAuthor iauthor, IPublicsher ipublicsher, IBibliographicmaterial ibibliographicmaterial, IUser iuser, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _author = iauthor;
            _publicsher = ipublicsher;
            _bibliographicmaterial = ibibliographicmaterial;
            _user = iuser;

        }
        ///<summary>
        ///вызов представления Catalog
        /// </summary>
        [HttpGet]
        public ViewResult Catalog()
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllAuthors = _author.AllAuthors.OrderBy(a => a.FullName); 
            libraryobj.AllPublishers = _publicsher.AllPublicshers.OrderBy(a => a.Name); 
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.AllBibliographicmaterial.OrderBy(a => a.Name); 
            return View(libraryobj);

        }
        [HttpPost]
        public IActionResult Catalog(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null, string sortBy= null)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            if (sortBy == "true")
            {
                libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.AllAuthors.OrderBy(a => a.FullName) : _author.SelectAuthor(nameAuthor).OrderBy(a => a.FullName);
                libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.AllPublicshers.OrderBy(a => a.Name) : _publicsher.SelectPublisher(namePublisher).OrderBy(a => a.Name);
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Name);
            }
            else
            {
                libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.AllAuthors.OrderByDescending(a => a.FullName) : _author.SelectAuthor(nameAuthor).OrderByDescending(a => a.FullName);
                libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.AllPublicshers.OrderByDescending(a => a.Name) : _publicsher.SelectPublisher(namePublisher).OrderByDescending(a => a.Name);
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderByDescending(a => a.Name);
            }

         

            return View(libraryobj);

        }
        [HttpGet]
        public ViewResult CatalogAdmin()
        {

            AllLibraryModels libraryobj = new AllLibraryModels();

            libraryobj.AllAuthors = _author.AllAuthors.OrderBy(a=>a.FullName);
            libraryobj.AllPublishers = _publicsher.AllPublicshers.OrderBy(a=>a.Name);
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.AllBibliographicmaterial.OrderBy(a=>a.Name);
            return View(libraryobj);

        }
        [HttpPost]
        public IActionResult CatalogAdmin(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null, string sortBy= null)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            if (sortBy == "true")
            {
                libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.AllAuthors.OrderBy(a => a.FullName) : _author.SelectAuthor(nameAuthor).OrderBy(a => a.FullName);
                libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.AllPublicshers.OrderBy(a => a.Name) : _publicsher.SelectPublisher(namePublisher).OrderBy(a => a.Name);
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Name);
            }
            else
            {
                libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.AllAuthors.OrderBy(a => a.FullName) : _author.SelectAuthor(nameAuthor).OrderByDescending(a => a.FullName);
                libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.AllPublicshers.OrderBy(a => a.Name) : _publicsher.SelectPublisher(namePublisher).OrderBy(a => a.Name);
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Name);
            }

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

        public IActionResult PageBibliographicmaterialAdminRedaction(int idBibliographicmaterial, string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null)
        {
           string connectionString = "Data Source=Catalogsdata.db";






            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
            databaseHelper.UpdateBibliographicmaterial(idBibliographicmaterial,nameBibliographicmaterial, nameAuthor, namePublisher ,  date);

            return RedirectToAction("CatalogAdmin", "Home");
            

        }
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedactionAuthor(int idAuthor , string nameAuthor = null, string contactsAuthor = null, string informationAuthor = null)
        {
            string connectionString = "Data Source=Catalogsdata.db";






            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
            databaseHelper.UpdateAuthor(idAuthor, nameAuthor, contactsAuthor, informationAuthor);
          
            return RedirectToAction("CatalogAdmin", "Home");


        }
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedactionPublisher(int idPublisher, string namePublisher = null, string contactsPublisher = null, string addressPublisher = null)
        {
            string connectionString = "Data Source=Catalogsdata.db";






            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
            databaseHelper.UpdatePublisher(idPublisher, namePublisher, contactsPublisher, addressPublisher);

            return RedirectToAction("CatalogAdmin", "Home");


        }
        [HttpPost]
        public IActionResult DeleteBibliographicmaterial(int idBibliographicmaterial)
        {
            string connectionString = "Data Source=Catalogsdata.db";






            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
            databaseHelper.DeleteBibliographicmaterial(idBibliographicmaterial);
            return RedirectToAction("CatalogAdmin", "Home");
        }
        [HttpPost]
        public IActionResult DeleteAuthor(int idAuthor)
        {
            string connectionString = "Data Source=Catalogsdata.db";






            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
            databaseHelper.DeleteAuthor(idAuthor);
            return RedirectToAction("CatalogAdmin", "Home");
        }
        [HttpPost]
        public IActionResult DeletePublisher(int idPublisher)
        {
            string connectionString = "Data Source=Catalogsdata.db";






            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
            databaseHelper.DeletePublisher(idPublisher);
            return RedirectToAction("CatalogAdmin", "Home");
        }

        [HttpGet]
        public ViewResult AddAuthor()
        {
           



            return View();
        }
        [HttpPost]
        public IActionResult AddAuthor(string fullname, string contacts, string information)
        {
            Author newAuthor = new Author()
            {
                FullName = fullname,
                Contacts = contacts,
                Information = information
            };
            string connectionString = "Data Source=Catalogsdata.db";
            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
            // добавляение нового Author в таблицу
            databaseHelper.AddAuthor(newAuthor);



            return View();
        }
        [HttpGet]
        public ViewResult AddPublisher()
        {




            return View();
        }
        [HttpPost]
        public IActionResult AddPublisher(string name, string contacts, string address)
        {
            Publisher newPublisher = new Publisher()
            {
                Name = name,
                Contacts = contacts,
                Address = address
            };

            string connectionString = "Data Source=Catalogsdata.db";
            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
            databaseHelper.AddPublisher(newPublisher);



            return View();
        }
        [HttpGet]
        public ViewResult CreatePageBibliographicmaterialAdmin()
        {
            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllAuthors = _author.AllAuthors;
            libraryobj.AllPublishers = _publicsher.AllPublicshers;
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.AllBibliographicmaterial;
            return View(libraryobj);
          
        }
        [HttpPost]
        public IActionResult CreatePageBibliographicmaterialAdmin(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null,IFormFile img=null)
        {
            string connectionString = "Data Source=Catalogsdata.db";
           
           /* if (img != null && img.Length > 0)
            {
                var filePath = Path.Combine(_appEnvironment.WebRootPath, "img", img.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(fileStream);
                }

            }*/




            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);

            Bibliographicmaterial newMaterial = new Bibliographicmaterial()
            {
                Name = nameBibliographicmaterial,
                Date = date,
                Img = "pict1.jpg",
            Author = new Author()
                {
                    Id = int.Parse(nameAuthor)
                },
                Publisher = new Publisher()
                {
                    Id = int.Parse(namePublisher)
                }
            };

            databaseHelper.AddBibliographicMaterial(newMaterial);
            return RedirectToAction("CatalogAdmin", "Home");


        }
        [HttpPost]
        public IActionResult SortBibliographicmaterial(string nameBibliographicmaterial)
        {
            string connectionString = "Data Source=Catalogsdata.db";






            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
            databaseHelper.SortBibliographicmaterial(nameBibliographicmaterial);
            return RedirectToAction("CatalogAdmin", "Home");
        }
    }
}
