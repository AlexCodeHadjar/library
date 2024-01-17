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
using library.BusinessLogic;

namespace library.Controllers
{
    ///<summary>
    ///контролер для работы с предстваление Catalog 
    /// </summary>
    public class HomeController : Controller
    {
        private const string CONNECTIONSTRING  = "Data Source=Catalogsdata.db";

        private readonly IServiceProvider _serviceProvider;

        private readonly IDataBaseHelperModels<Author> _authorServices;
     
        private readonly IDataBaseHelperModels<Publisher> _publisherServices;
       
        private readonly IDataBaseHelperModels<BibliographicMaterial> _bibliographicmaterialServices;
        private readonly IWebHostEnvironment _appEnvironment;

        private readonly IBusinessLogicCatalog _libraryServices;

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
            return View(_libraryServices.StartLibraryModels());
     

        }
        ///<summary>
        ///получения данных из формы  представления 
        /// </summary>
        [HttpPost]
        public IActionResult Catalog(string nameBibliographicmaterial , string nameAuthor, string namePublisher, string date , DatabaseHelper.SortBy sortBy)
        {
            return View(_libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy));
           
        }

    
    ///<summary>
    ///вызов представления CatalogAdmin для админа
    /// </summary>
    [HttpGet]
        public ViewResult CatalogAdmin()
        {
                return View(_libraryServices.StartLibraryModels());
           

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
        public IActionResult CatalogAdmin(string nameBibliographicmaterial , string nameAuthor , string namePublisher , string date, DatabaseHelper.SortBy sortBy )
        {
            return View(_libraryServices.SortLibraryModels(nameBibliographicmaterial, nameAuthor, namePublisher, date, sortBy));
          
        }





        /// <summary>
        /// Вызов представления информации о BibliographicMaterial для пользователя
        /// </summary>
        /// <param name="materialId">ID</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult PageBibliographicmaterial(int materialId)
        {

            return View(_libraryServices.PageBibliographicmaterial(materialId));
   

            
        }
        /// <summary>
        /// Вызов представления информации о BibliographicMaterial для пользователя
        /// </summary>
        /// <param name="materialId">ID</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult PageBibliographicmaterialAdmin(int materialId)
        {

            return View(_libraryServices.PageBibliographicmaterialAdmin(materialId));
          
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

        public IActionResult PageBibliographicmaterialAdminRedaction(BibliographicMaterial material,IFormFile? file= null)
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
            _bibliographicmaterialServices.Update(material);

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

        public IActionResult PageBibliographicmaterialAdminRedactionAuthor(Author author)
        {
            _authorServices.Update(author);

 
            return RedirectToAction("CatalogAdmin", "Home");
        }
        /// <summary>
        /// Изменяет  информацию о Publisher 
        /// </summary>
        /// <param name="idPublisher">ID</param>
        /// <param name="namePublisher">Название</param>
        /// <param name="contactsPublisher">Контакты</param>
        /// <param name="InsertressPublisher">Адрес</param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedactionPublisher(Publisher publisher)
        {


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
            _bibliographicmaterialServices.Delete(idBibliographicmaterial); 


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
            _authorServices.Delete(idAuthor);


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
            _publisherServices.Delete(idPublisher);


            return RedirectToAction("CatalogAdmin", "Home");
        }

        /// <summary>
        /// Вызывает представление дабавления автора 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult InsertAuthor()
        {

            return View();

        }

       /// <summary>
       /// Добавиление в базу данных автора
       /// </summary>
       /// <param name="author"> Параметры автора</param>
       /// <returns></returns>
        [HttpPost]
        public IActionResult InsertAuthor(Author author)
        {
            _authorServices.Insert(author);


            return View();
        }

        /// <summary>
        /// Вызывает представление дабавления издательства 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult InsertPublisher()
        {
            return View();
        }

        /// <summary>
        /// получает форму из представление для дабавления издательства 
        /// </summary>
        /// <param name="newPublisher">Данные издательтсва</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertPublisher(Publisher newPublisher)
        {
            _publisherServices.Insert(newPublisher);


            return View();
        }

        /// <summary>
        /// Открывает страцину для создания BibliographicMaterial
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult CreatePageBibliographicmaterialAdmin()
        {
            return View(_libraryServices.StartLibraryModels());

          
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
        public IActionResult CreatePageBibliographicmaterialAdmin(BibliographicMaterial bibliographicMaterial , IFormFile? img)
        {
            /* if (img != null && img.Length > 0)
             {
                 var filePath = Path.Combine(_appEnvironment.WebRootPath, "img", img.FileName);
                 using (var fileStream = new FileStream(filePath, FileMode.Create))
                 {
                     img.CopyTo(fileStream);
                 }

             }*/
            _bibliographicmaterialServices.Insert(bibliographicMaterial);
           
            return RedirectToAction("CatalogAdmin", "Home");


        }
        
    }
}
