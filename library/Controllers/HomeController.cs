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
    ///��������� ��� ������ � ������������� Catalog 
    /// </summary>
    public class HomeController : Controller
    {
        const string CONNECTIONSTRING  = "Data Source=Catalogsdata.db";
        private DatabaseHelper _databaseHelper = new DatabaseHelper(CONNECTIONSTRING);
       

        ///<summary>
        ///_author ����������� �������� ���������� ���������� IDataBaseHelperAuthor
        /// </summary>
        private readonly IDataBaseHelperAuthor _author;

        ///<summary>
        ///_publicsher ����������� �������� ���������� ���������� IDataBaseHelperPublisher
        /// </summary>
        private readonly IDataBaseHelperPublisher _publicsher;

        ///<summary>
        ///_bibliographicmaterial ����������� �������� ���������� ���������� IDataBaseHelperBibliographicmaterial
        /// </summary>
        private readonly IDataBaseHelperBibliographicmaterial _bibliographicmaterial;
        ///<summary>
        ///_bibliographicmaterial ����������� �������� ���������� ���������� IDataBaseHelperUser
        /// </summary>
      
        private readonly IWebHostEnvironment _appEnvironment;


        ///<summary>
        ///IAuthor iauthor �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
        ///IPublicsher ipublicsher �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
        /// IBibliographicmaterial ibibliographicmaterial �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
        /// </summary>
        public HomeController(IDataBaseHelperAuthor iauthor, IDataBaseHelperPublisher ipublicsher, IDataBaseHelperBibliographicmaterial ibibliographicmaterial, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _author = iauthor;
            _publicsher = ipublicsher;
            _bibliographicmaterial = ibibliographicmaterial;
        
        }
        ///<summary>
        ///����� ������������� Catalog ��� ������������
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
        ///<summary>
        ///��������� ������ �� �����  ������������� 
        /// </summary>
        [HttpPost]
        public IActionResult Catalog(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null, DatabaseHelper.SortBy sortBy= null)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher);
            if (sortBy.SortNameAuthor)
            {

                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Author.FullName);
            }


            if (sortBy.SortNamePublisher)
            {
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Publisher.Name);

            }

            if (sortBy.SortNameBibliographicmaterial)
            {
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Name);
            }

            if (sortBy.SortDate)
            {
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderByDescending(a => a.Date);
            }

            libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.SelectPublisher().OrderBy(a => a.Name) : _publicsher.SelectPublisher(namePublisher).OrderBy(a => a.Name);
            libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.SelectAuthor().OrderBy(a => a.FullName) : _author.SelectAuthor(nameAuthor).OrderBy(a => a.FullName);

            return View(libraryobj);
        }

    
    ///<summary>
    ///����� ������������� CatalogAdmin ��� ������
    /// </summary>
    [HttpGet]
        public ViewResult CatalogAdmin()
        {

            AllLibraryModels libraryobj = new AllLibraryModels();

            libraryobj.AllAuthors = _author.SelectAuthor().OrderBy(a=>a.FullName);
            libraryobj.AllPublishers = _publicsher.SelectPublisher().OrderBy(a=>a.Name);
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial().OrderBy(a=>a.Name);
            return View(libraryobj);

        }
        /// <summary>
        /// ��������� ������ �� �����  �������������  ������
        /// </summary>
        /// <param name="nameBibliographicmaterial"> �������� BibliographicMaterial</param>
        /// <param name="nameAuthor">��� ������</param>
        /// <param name="namePublisher">�������� ������������</param>
        /// <param name="date"> ��� �������</param>
        /// <param name="sortBy"> ����������</param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult CatalogAdmin(string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null, DatabaseHelper.SortBy sortBy = null)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher);
            if (sortBy.SortNameAuthor)
            {

                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Author.FullName);
            }


            if (sortBy.SortNamePublisher)
            {
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Publisher.Name);

            }

            if (sortBy.SortNameBibliographicmaterial)
            {
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Name);
            }

            if (sortBy.SortDate)
            {
                libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderByDescending(a => a.Date);
            }

            libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _publicsher.SelectPublisher().OrderBy(a => a.Name) : _publicsher.SelectPublisher(namePublisher).OrderBy(a => a.Name);
            libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _author.SelectAuthor().OrderBy(a => a.FullName) : _author.SelectAuthor(nameAuthor).OrderBy(a => a.FullName);

            return View(libraryobj);
        }





        /// <summary>
        /// ����� ������������� ���������� � BibliographicMaterial ��� ������������
        /// </summary>
        /// <param name="materialId">ID</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult PageBibliographicmaterial(int materialId)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            IEnumerable<BibliographicMaterial> allBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial();


            libraryobj.AllBibliographicmaterial = allBibliographicmaterial.Where(a => a.Id == materialId);
            return View(libraryobj);
        }
        /// <summary>
        /// ����� ������������� ���������� � BibliographicMaterial ��� ������������
        /// </summary>
        /// <param name="materialId">ID</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult PageBibliographicmaterialAdmin(int materialId)
        {

            AllLibraryModels libraryobj = new AllLibraryModels();
            IEnumerable<BibliographicMaterial> allBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial();
            libraryobj.AllAuthors = _author.SelectAuthor();
            libraryobj.AllPublishers = _publicsher.SelectPublisher();
       

            libraryobj.AllBibliographicmaterial = allBibliographicmaterial.Where(a => a.Id == materialId);
            return View(libraryobj);
        }
        /// <summary>
        /// �������� BibliographicMaterial  ���������� � BibliographicMaterial ��� ������������
        /// </summary>
        /// <param name="idBibliographicmaterial">ID</param>
        /// <param name="nameBibliographicmaterial">�������� BibliographicMaterial</param>
        /// <param name="nameAuthor">��� ������</param>
        /// <param name="namePublisher">�������� ������������</param>
        /// <param name="date">��� </param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedaction(int idBibliographicmaterial, string nameBibliographicmaterial = null, string nameAuthor = null, string namePublisher = null, string date = null,IFormFile file= null)
        {
            if (file != null)
            {
                var filePath = Path.Combine(_appEnvironment.WebRootPath, "img", file.FileName);

               
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                

            }


            DatabaseHelper.DataBaseBibliographicmaterial dataBaseBibliographicmaterial = new();
            dataBaseBibliographicmaterial.UpdateBibliographicmaterial(idBibliographicmaterial,nameBibliographicmaterial, nameAuthor, namePublisher ,  date);

            return RedirectToAction("CatalogAdmin", "Home");
            

        }
        /// <summary>
        /// ��������  ���������� � Author 
        /// </summary>
        /// <param name="idAuthor">ID</param>
        /// <param name="nameAuthor">��� ������</param>
        /// <param name="contactsAuthor">�������� </param>
        /// <param name="informationAuthor">����������</param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedactionAuthor(int idAuthor , string nameAuthor = null, string contactsAuthor = null, string informationAuthor = null)
        {

            _author.UpdateAuthor(idAuthor, nameAuthor, contactsAuthor, informationAuthor);
          
            return RedirectToAction("CatalogAdmin", "Home");


        }
        /// <summary>
        /// ��������  ���������� � Publisher 
        /// </summary>
        /// <param name="idPublisher">ID</param>
        /// <param name="namePublisher">��������</param>
        /// <param name="contactsPublisher">��������</param>
        /// <param name="addressPublisher">�����</param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult PageBibliographicmaterialAdminRedactionPublisher(int idPublisher, string namePublisher = null, string contactsPublisher = null, string addressPublisher = null)
        {

            DatabaseHelper.DataBasePublisher publisher = new();
            publisher.UpdatePublisher(idPublisher, namePublisher, contactsPublisher, addressPublisher);

            return RedirectToAction("CatalogAdmin", "Home");


        }
        /// <summary>
        /// �������� BibliographicMaterial �� ��
        /// </summary>
        /// <param name="idBibliographicmaterial">ID</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteBibliographicmaterial(int idBibliographicmaterial)
        {

            DatabaseHelper.DataBaseBibliographicmaterial dataBaseBibliographicmaterial= new();
            dataBaseBibliographicmaterial.DeleteBibliographicmaterial(idBibliographicmaterial);
            return RedirectToAction("CatalogAdmin", "Home");
        }
        /// <summary>
        /// �������� Author �� ��
        /// </summary>
        /// <param name="idAuthor">ID</param>
        /// <returns></returns>
        [HttpPost]
        
        public IActionResult DeleteAuthor(int idAuthor)
        {

            _author.DeleteAuthor(idAuthor);
            return RedirectToAction("CatalogAdmin", "Home");
        }
        /// <summary>
        /// �������� Publisher �� �� 
        /// </summary>
        /// <param name="idPublisher">ID</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeletePublisher(int idPublisher)
        {
            DatabaseHelper.DataBasePublisher publisher = new();

            publisher.DeletePublisher(idPublisher);

            return RedirectToAction("CatalogAdmin", "Home");
        }
        /// <summary>
        /// �������� ������������� ���������� ������ 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult AddAuthor()
        {
           



            return View();
        }
        /// <summary>
        /// ���������� ������ � ��
        /// </summary>
        /// <param name="fullname">���</param>
        /// <param name="contacts">��������</param>
        /// <param name="information">����������</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddAuthor(string fullname, string contacts, string information)
        {
            Author newAuthor = new Author()
            {
                FullName = fullname,
                Contacts = contacts,
                Information = information
            };

            // ����������� ������ Author � �������
            _author.AddAuthor(newAuthor);



            return View();
        }
        /// <summary>
        /// �������� ������������� ���������� ������������ 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult AddPublisher()
        {




            return View();
        }
        /// <summary>
        /// ���������� ������������ � ��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="contacts">��������</param>
        /// <param name="address">�����</param>
        /// <returns></returns>
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
        /// <summary>
        /// ��������� �������� ��� �������� BibliographicMaterial
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult CreatePageBibliographicmaterialAdmin()
        {
            AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllAuthors = _author.SelectAuthor();
            libraryobj.AllPublishers = _publicsher.SelectPublisher();
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.SelectBibliographicmaterial();
            return View(libraryobj);
          
        }
        /// <summary>
        /// �������� �����  ��� �������� BibliographicMaterial
        /// </summary>
        /// <param name="nameBibliographicmaterial">��������</param>
        /// <param name="nameAuthor">��� ������</param>
        /// <param name="namePublisher">�������� ������������</param>
        /// <param name="date">��� �������</param>
        /// <param name="img">��������</param>
        /// <returns></returns>
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

            BibliographicMaterial newMaterial = new BibliographicMaterial()
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







            // �������� ������ �����������
            DatabaseHelper databaseHelper = new DatabaseHelper(CONNECTIONSTRING);
           // databaseHelper.SortBibliographicmaterial(nameBibliographicmaterial);
            return RedirectToAction("CatalogAdmin", "Home");
        }
    }
}
