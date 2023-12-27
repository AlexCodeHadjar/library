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
        ///��������� ��� ������ � ������������� Catalog 
        /// </summary>
        private readonly IAuthor _author ;
        ///<summary>
        ///_author ����������� �������� ���������� ���������� IAuthor
        /// </summary>
        private readonly IPublicsher _publicsher;
        ///<summary>
        ///_publicsher ����������� �������� ���������� ���������� IPublicsher
        /// </summary>
        private readonly IBibliographicmaterial _bibliographicmaterial;
        ///<summary>
        ///_bibliographicmaterial ����������� �������� ���������� ���������� IBibliographicmaterial
        /// </summary>
        public HomeController(IAuthor iauthor, IPublicsher ipublicsher, IBibliographicmaterial ibibliographicmaterial)
        {
            ///<summary>
            ///IAuthor iauthor �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
            ///IPublicsher ipublicsher �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
            /// IBibliographicmaterial ibibliographicmaterial �������� ��������� � ����� ��������� �� ���� (��������� ������ �������� � �������)
            /// </summary>
            _author = iauthor;
            _publicsher = ipublicsher;
            _bibliographicmaterial = ibibliographicmaterial;
            
        }
        public ViewResult Catalog()
        {
            ///<summary>
            ///����� � �������� ������ � ������������� Catalog
            /// </summary>
        AllLibraryModels libraryobj = new AllLibraryModels();
            libraryobj.getallauthors = _author.AllAuthors;
            libraryobj.getallpublishers = _publicsher.Allpublicshers;
            libraryobj.getallBibliographicmaterial = _bibliographicmaterial.Allbibliographicmaterial;
            return View(libraryobj);
            
        }

    }
}
