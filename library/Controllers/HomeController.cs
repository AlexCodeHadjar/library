using library.Data.Models;
using library.ViewModels;
using Library.Data.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace library.Controllers
{
    public class HomeController : Controller
    {
        ///<summary>
        ///контролер для работы с предстваление Catalog 
        /// </summary>
        private readonly IAuthor _author ;
        ///<summary>
        ///_author переменнная принимет реализацию интерфейса IAuthor
        /// </summary>
        private readonly IPublicsher _publicsher;
        ///<summary>
        ///_publicsher переменнная принимет реализацию интерфейса IPublicsher
        /// </summary>
        private readonly IBibliographicmaterial _bibliographicmaterial;
        ///<summary>
        ///_bibliographicmaterial переменнная принимет реализацию интерфейса IBibliographicmaterial
        /// </summary>
        public HomeController(IAuthor iauthor, IPublicsher ipublicsher, IBibliographicmaterial ibibliographicmaterial)
        {
            ///<summary>
            ///IAuthor iauthor передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
            ///IPublicsher ipublicsher передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
            /// IBibliographicmaterial ibibliographicmaterial передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
            /// </summary>
            _author = iauthor;
            _publicsher = ipublicsher;
            _bibliographicmaterial = ibibliographicmaterial;
            
        }
        public ViewResult Catalog()
        {
            ///<summary>
            ///вызов и передача данных в представление Catalog
            /// </summary>
        AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.getallauthors = _author.AllAuthors;
            libraryobj.getallpublishers = _publicsher.Allpublicshers;
            libraryobj.getallBibliographicmaterial = _bibliographicmaterial.Allbibliographicmaterial;
            return View(libraryobj);
            
        }

    }
}
