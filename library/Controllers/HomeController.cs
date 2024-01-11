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
        const string CONNECTIONSTRING  = "Data Source=Catalogsdata.db";
        private DatabaseHelper _databaseHelper = new DatabaseHelper(CONNECTIONSTRING);


        ///<summary>
        ///_author переменнная принимет реализацию интерфейса IDataBaseHelperAuthor
        /// </summary>
        private readonly IDataBaseHelperAuthor _author;

        ///<summary>
        ///_publicsher переменнная принимет реализацию интерфейса IDataBaseHelperPublisher
        /// </summary>
        private readonly IDataBaseHelperPublisher _publicsher;

        ///<summary>
        ///_bibliographicmaterial переменнная принимет реализацию интерфейса IDataBaseHelperBibliographicmaterial
        /// </summary>
        private readonly IDataBaseHelperBibliographicmaterial _bibliographicmaterial;
        ///<summary>
        ///_bibliographicmaterial переменнная принимет реализацию интерфейса IDataBaseHelperUser
        /// </summary>
        private readonly IDataBaseHelperUser _user;
        private readonly IWebHostEnvironment _appEnvironment;


        ///<summary>
        ///IAuthor iauthor передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
        ///IPublicsher ipublicsher передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
        /// IBibliographicmaterial ibibliographicmaterial передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
        /// </summary>
        public HomeController(IDataBaseHelperAuthor iauthor, IDataBaseHelperPublisher ipublicsher, IDataBaseHelperBibliographicmaterial ibibliographicmaterial, IDataBaseHelperUser iuser, IWebHostEnvironment appEnvironment)
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
            libraryobj.AllAuthors = _author.SelectAuthor().OrderBy(a => a.FullName); 
            libraryobj.AllPublishers = _publicsher.SelectPublisher().OrderBy(a => a.Name); 
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial().OrderBy(a => a.Name); 
            return View(libraryobj);

        }
        [HttpPost]
        public IActionResult Catalog(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null, string sortBy= null)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            if (sortBy == "true")
            {
                libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.SelectAuthor().OrderBy(a => a.FullName) : _author.SelectAuthor(nameAuthor).OrderBy(a => a.FullName);
                libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.SelectPublisher().OrderBy(a => a.Name) : _publicsher.SelectPublisher(namePublisher).OrderBy(a => a.Name);
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Name);
            }
            else
            {
                libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.SelectAuthor().OrderByDescending(a => a.FullName) : _author.SelectAuthor(nameAuthor).OrderByDescending(a => a.FullName);
                libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.SelectPublisher().OrderByDescending(a => a.Name) : _publicsher.SelectPublisher(namePublisher).OrderByDescending(a => a.Name);
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderByDescending(a => a.Name);
            }

         

            return View(libraryobj);

        }
        [HttpGet]
        public ViewResult CatalogAdmin()
        {

            AllLibraryModels libraryobj = new AllLibraryModels();

            libraryobj.AllAuthors = _author.SelectAuthor().OrderBy(a=>a.FullName);
            libraryobj.AllPublishers = _publicsher.SelectPublisher().OrderBy(a=>a.Name);
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial().OrderBy(a=>a.Name);
            return View(libraryobj);

        }
        [HttpPost]
        public IActionResult CatalogAdmin(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null, string sortBy= null)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            if (sortBy == "true")
            {
                libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.SelectAuthor().OrderBy(a => a.FullName) : _author.SelectAuthor(nameAuthor).OrderBy(a => a.FullName);
                libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.SelectPublisher().OrderBy(a => a.Name) : _publicsher.SelectPublisher(namePublisher).OrderBy(a => a.Name);
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Name);
            }
            else
            {
                libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.SelectAuthor().OrderBy(a => a.FullName) : _author.SelectAuthor(nameAuthor).OrderByDescending(a => a.FullName);
                libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.SelectPublisher().OrderBy(a => a.Name) : _publicsher.SelectPublisher(namePublisher).OrderBy(a => a.Name);
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Name);
            }

            return View(libraryobj);

        }
        [HttpPost]
        public ViewResult PageBibliographicmaterial(int materialId)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            IEnumerable<Bibliographicmaterial> allBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial();


            libraryobj.AllBibliographicmaterial = allBibliographicmaterial.Where(a => a.Id == materialId);
            return View(libraryobj);
        }
        [HttpPost]
        public ViewResult PageBibliographicmaterialAdmin(int materialId)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            IEnumerable<Bibliographicmaterial> allBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial();
            libraryobj.AllAuthors = _author.SelectAuthor();
            libraryobj.AllPublishers = _publicsher.SelectPublisher();
       

            libraryobj.AllBibliographicmaterial = allBibliographicmaterial.Where(a => a.Id == materialId);
            return View(libraryobj);
        }
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedaction(int idBibliographicmaterial, string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null)
        {
       
            DatabaseHelper.DataBaseBibliographicmaterial dataBaseBibliographicmaterial = new();
            dataBaseBibliographicmaterial.UpdateBibliographicmaterial(idBibliographicmaterial,nameBibliographicmaterial, nameAuthor, namePublisher ,  date);

            return RedirectToAction("CatalogAdmin", "Home");
            

        }
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedactionAuthor(int idAuthor , string nameAuthor = null, string contactsAuthor = null, string informationAuthor = null)
        {

            _author.UpdateAuthor(idAuthor, nameAuthor, contactsAuthor, informationAuthor);
          
            return RedirectToAction("CatalogAdmin", "Home");


        }
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedactionPublisher(int idPublisher, string namePublisher = null, string contactsPublisher = null, string addressPublisher = null)
        {

            DatabaseHelper.DataBasePublisher publisher = new();
            publisher.UpdatePublisher(idPublisher, namePublisher, contactsPublisher, addressPublisher);

            return RedirectToAction("CatalogAdmin", "Home");


        }
        [HttpPost]
        public IActionResult DeleteBibliographicmaterial(int idBibliographicmaterial)
        {

            DatabaseHelper.DataBaseBibliographicmaterial dataBaseBibliographicmaterial= new();
            dataBaseBibliographicmaterial.DeleteBibliographicmaterial(idBibliographicmaterial);
            return RedirectToAction("CatalogAdmin", "Home");
        }
        [HttpPost]
        public IActionResult DeleteAuthor(int idAuthor)
        {

            _author.DeleteAuthor(idAuthor);
            return RedirectToAction("CatalogAdmin", "Home");
        }
        [HttpPost]
        public IActionResult DeletePublisher(int idPublisher)
        {
            DatabaseHelper.DataBasePublisher publisher = new();

            publisher.DeletePublisher(idPublisher);

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

            // добавляение нового Author в таблицу
            _author.AddAuthor(newAuthor);



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
            DatabaseHelper.DataBasePublisher publisher = new();
            publisher.AddPublisher(newPublisher);



            return View();
        }
        [HttpGet]
        public ViewResult CreatePageBibliographicmaterialAdmin()
        {
            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllAuthors = _author.SelectAuthor();
            libraryobj.AllPublishers = _publicsher.SelectPublisher();
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial();
            return View(libraryobj);
          
        }
        [HttpPost]
        public IActionResult CreatePageBibliographicmaterialAdmin(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null,IFormFile img=null)
        {

           
           /* if (img != null && img.Length > 0)
            {
                var filePath = Path.Combine(_appEnvironment.WebRootPath, "img", img.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(fileStream);
                }

            }*/





            DatabaseHelper.DataBaseBibliographicmaterial dataBaseBibliographicmaterial = new();

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

            dataBaseBibliographicmaterial.AddBibliographicMaterial(newMaterial);
            return RedirectToAction("CatalogAdmin", "Home");


        }
        [HttpPost]
        public IActionResult SortBibliographicmaterial(string nameBibliographicmaterial)
        {







            // передача строки подключения
            DatabaseHelper databaseHelper = new DatabaseHelper(CONNECTIONSTRING);
            databaseHelper.SortBibliographicmaterial(nameBibliographicmaterial);
            return RedirectToAction("CatalogAdmin", "Home");
        }
    }
}
