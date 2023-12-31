using library.Data.Models;
using library.ViewModels;
using library.Data.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace library.Controllers
{
    ///<summary>
    ///контролер для работы с предстваление Catalog 
    /// </summary>
    public class HomeController : Controller
    {
        
        ///<summary>
        ///_author переменнная принимет реализацию интерфейса IAuthor
        /// </summary>
        private readonly IAuthor _author ;
      
         ///<summary>
        ///_publicsher переменнная принимет реализацию интерфейса IPublicsher
        /// </summary>
        private readonly IPublicsher _publicsher;
      
        ///<summary>
        ///_bibliographicmaterial переменнная принимет реализацию интерфейса IBibliographicmaterial
        /// </summary>
        private readonly IBibliographicmaterial _bibliographicmaterial;
        
  
        ///<summary>
        ///IAuthor iauthor передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
        ///IPublicsher ipublicsher передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
        /// IBibliographicmaterial ibibliographicmaterial передаем интерфейс и класс релизации на него (интерфейс связан сервисом с классом)
        /// </summary>
        public HomeController(IAuthor iauthor, IPublicsher ipublicsher, IBibliographicmaterial ibibliographicmaterial)
        {
           
            _author = iauthor;
            _publicsher = ipublicsher;
            _bibliographicmaterial = ibibliographicmaterial;
            
        }
        ///<summary>
        ///вызов представления Catalog
        /// </summary>
        public ViewResult Catalog()
        {
            
        AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.AllAuthors = _author.AllAuthors;
            libraryobj.AllPublishers = _publicsher.AllPublicshers;
            libraryobj.AllBibliographicmaterial = _bibliographicmaterial.AllBibliographicmaterial;
            return View(libraryobj);
            
        }
       

    }
}
