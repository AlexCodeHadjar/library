using library.Data.Models;
using library.ViewModels;
using library.Data.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace library.Controllers
{
    ///<summary>
    ///��������� ��� ������ � ������������� Catalog 
    /// </summary>
    public class HomeController : Controller
    {
        
        ///<summary>
        ///_author ����������� �������� ���������� ���������� IAuthor
        /// </summary>
        private readonly IAuthor _author ;
      
         ///<summary>
        ///_publicsher ����������� �������� ���������� ���������� IPublicsher
        /// </summary>
        private readonly IPublicsher _publicsher;
      
        ///<summary>
        ///_bibliographicmaterial ����������� �������� ���������� ���������� IBibliographicmaterial
        /// </summary>
        private readonly IBibliographicmaterial _bibliographicmaterial;
        
  
        ///<summary>
        ///IAuthor iauthor �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
        ///IPublicsher ipublicsher �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
        /// IBibliographicmaterial ibibliographicmaterial �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
        /// </summary>
        public HomeController(IAuthor iauthor, IPublicsher ipublicsher, IBibliographicmaterial ibibliographicmaterial)
        {
           
            _author = iauthor;
            _publicsher = ipublicsher;
            _bibliographicmaterial = ibibliographicmaterial;
            
        }
        ///<summary>
        ///����� ������������� Catalog
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
