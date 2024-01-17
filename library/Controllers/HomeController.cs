using library.Data.Models;
using library.ViewModels;
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

        private readonly IServiceProvider _serviceProvider;

    
      
        private readonly IWebHostEnvironment _appEnvironment;


      
        public HomeController(IServiceProvider serviceProvider, IWebHostEnvironment appEnvironment)
        {
            _serviceProvider = serviceProvider;
            _appEnvironment = appEnvironment;
            _libraryServices = _serviceProvider.GetRequiredService<IBusinessLogicCatalog>();
            _authorServices = _serviceProvider.GetRequiredService<IDataBaseHelperModels<Author>>();
            _publisherServices = _serviceProvider.GetRequiredService<IDataBaseHelperModels<Publisher>>();
            _bibliographicmaterialServices = _serviceProvider.GetRequiredService<IDataBaseHelperModels<BibliographicMaterial>>();

          
   
          
        }
        ///<summary>
        ///вызов представления Catalog для пользователя
        /// </summary>
        [HttpGet]
        public ViewResult Catalog()
        {
     
            return View(_serviceProvider.GetRequiredService<IDataBaseHelperLibraryCatolog>().StartLibraryModels());

        }
        ///<summary>
        ///получения данных из формы  представления 
        /// </summary>
        [HttpPost]
        public IActionResult Catalog(string? nameBibliographicmaterial = null, string? nameAuthor = null, string? namePublisher = null, string? date = null, DatabaseHelper.SortBy? sortBy= null)
        {
          
            return View(_serviceProvider.GetRequiredService<IDataBaseHelperLibraryCatolog>().SortLibraryModels(nameBibliographicmaterial,  nameAuthor, namePublisher, date,  sortBy));
        }

    
    ///<summary>
    ///вызов представления CatalogAdmin для админа
    /// </summary>
    [HttpGet]
        public ViewResult CatalogAdmin()
        {
          
            return View(_serviceProvider.GetRequiredService<IDataBaseHelperLibraryCatolog>().StartLibraryModels());

        }
        /// <summary>
        /// получения данных из формы  представления  админа
        /// </summary>
        /// <param name="nameBibliographicmaterial"> Название BibliographicMaterial</param>
        /// <param name="nameAuthor">Имя автора</param>
        /// <param name="namePublisher">Название Издательства</param>
        /// <param name="date"> Год издания</param>
        /// <param name="sortBy"> Сортировка</param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult CatalogAdmin(string? nameBibliographicmaterial = null, string? nameAuthor = null, string? namePublisher = null, string? date = null, DatabaseHelper.SortBy? sortBy = null)
        {
            
            return View(_serviceProvider.GetRequiredService<IDataBaseHelperLibraryCatolog>().SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy));
        }





        /// <summary>
        /// Вызов представления информации о BibliographicMaterial для пользователя
        /// </summary>
        /// <param name="materialId">ID</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult PageBibliographicmaterial(int materialId)
        {

       
            return View(_serviceProvider.GetRequiredService<IDataBaseHelperLibraryCatolog>().PageBibliographicmaterial(materialId));

            
        }
        /// <summary>
        /// Вызов представления информации о BibliographicMaterial для пользователя
        /// </summary>
        /// <param name="materialId">ID</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult PageBibliographicmaterialAdmin(int materialId)
        {

           
            return View(_serviceProvider.GetRequiredService<IDataBaseHelperLibraryCatolog>().PageBibliographicmaterialAdmin(materialId));
        }
        /// <summary>
        /// Изменяет BibliographicMaterial  информации о BibliographicMaterial для пользователя
        /// </summary>
        /// <param name="idBibliographicmaterial">ID</param>
        /// <param name="nameBibliographicmaterial">Название BibliographicMaterial</param>
        /// <param name="nameAuthor">Имя автора</param>
        /// <param name="namePublisher">Название издательства</param>
        /// <param name="date">Год </param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedaction(int idBibliographicmaterial, string? nameBibliographicmaterial = null, string? nameAuthor = null, string? namePublisher = null, string? date = null,IFormFile? file= null)
        {
            /*
            if (file != null)
            {
                var filePath = Path.Combine(_appEnvironment.WebRootPath, "img", file.FileName);

               
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                

            }
            */    
            _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().UpdateBibliographicmaterial(idBibliographicmaterial, nameBibliographicmaterial, nameAuthor, namePublisher, date);
            return RedirectToAction("CatalogAdmin", "Home");
            

        }
        /// <summary>
        /// Изменяет  информацию о Author 
        /// </summary>
        /// <param name="idAuthor">ID</param>
        /// <param name="nameAuthor">Имя автора</param>
        /// <param name="contactsAuthor">Контакты </param>
        /// <param name="informationAuthor">Информация</param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedactionAuthor(int idAuthor , string? nameAuthor = null, string? contactsAuthor = null, string? informationAuthor = null)
        {
            _serviceProvider.GetRequiredService<IDataBaseHelperAuthor>().UpdateAuthor(idAuthor, nameAuthor, contactsAuthor, informationAuthor);
 
            return RedirectToAction("CatalogAdmin", "Home");
        }
        /// <summary>
        /// Изменяет  информацию о Publisher 
        /// </summary>
        /// <param name="idPublisher">ID</param>
        /// <param name="namePublisher">Название</param>
        /// <param name="contactsPublisher">Контакты</param>
        /// <param name="addressPublisher">Адрес</param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedactionPublisher(int idPublisher, string? namePublisher = null, string? contactsPublisher = null, string? addressPublisher = null)
        {

            _serviceProvider.GetRequiredService<IDataBaseHelperPublisher>().UpdatePublisher(idPublisher, namePublisher, contactsPublisher, addressPublisher);

            return RedirectToAction("CatalogAdmin", "Home");
        }
        /// <summary>
        /// Удаление BibliographicMaterial из бд
        /// </summary>
        /// <param name="idBibliographicmaterial">ID</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteBibliographicmaterial(int idBibliographicmaterial)
        {

            _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().DeleteBibliographicmaterial(idBibliographicmaterial);

            return RedirectToAction("CatalogAdmin", "Home");

        }

        /// <summary>
        /// Удаление Author из бд
        /// </summary>
        /// <param name="idAuthor">ID</param>
        /// <returns></returns>
        [HttpPost]
        
        public IActionResult DeleteAuthor(int idAuthor)
        {

            _serviceProvider.GetRequiredService<IDataBaseHelperAuthor>().DeleteAuthor(idAuthor);

            return RedirectToAction("CatalogAdmin", "Home");
        }
        /// <summary>
        /// удаление Publisher из бд 
        /// </summary>
        /// <param name="idPublisher">ID</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeletePublisher(int idPublisher)
        {

            _serviceProvider.GetRequiredService<IDataBaseHelperPublisher>().DeletePublisher(idPublisher);

            return RedirectToAction("CatalogAdmin", "Home");
        }

        /// <summary>
        /// Вызывает представление дабавления автора 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult AddAuthor()
        {

            return View();

        }

       /// <summary>
       /// Добавиление в базу данных автора
       /// </summary>
       /// <param name="author"> Параметры автора</param>
       /// <returns></returns>
        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {

            _serviceProvider.GetRequiredService<IDataBaseHelperAuthor>().AddAuthor(author);

            return View();
        }

        /// <summary>
        /// Вызывает представление дабавления издательства 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult AddPublisher()
        {
            return View();
        }

        /// <summary>
        /// Добавление издательства в бд
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="contacts">Контакты</param>
        /// <param name="address">Адрес</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddPublisher(Publisher newPublisher)
        {

            _serviceProvider.GetRequiredService<IDataBaseHelperPublisher>().AddPublisher(newPublisher);

            return View();
        }

        /// <summary>
        /// Открывает страцину для создания BibliographicMaterial
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult CreatePageBibliographicmaterialAdmin()
        {
            return View(_serviceProvider.GetRequiredService<IDataBaseHelperLibraryCatolog>().StartLibraryModels());
          
        }

        /// <summary>
        /// получает форму  для создания BibliographicMaterial
        /// </summary>
        /// <param name="nameBibliographicmaterial">Название</param>
        /// <param name="nameAuthor">Имя автора</param>
        /// <param name="namePublisher">Название издательства</param>
        /// <param name="date">Год издания</param>
        /// <param name="img">Картинка</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreatePageBibliographicmaterialAdmin(string? nameBibliographicmaterial = null, string? nameAuthor = null, string? namePublisher = null, string? date = null,IFormFile? img=null)
        {
           /* if (img != null && img.Length > 0)
            {
                var filePath = Path.Combine(_appEnvironment.WebRootPath, "img", img.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(fileStream);
                }

            }*/
            _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().AddBibliographicMaterial(new BibliographicMaterial()
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
            });
           
            return RedirectToAction("CatalogAdmin", "Home");


        }
        
    }
}
