using Microsoft.AspNetCore.Mvc;
using library.DataBase;
using library.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Policy;
using System.Diagnostics.Eventing.Reader;
using System.Data;
using static library.DataBase.DatabaseHelper;


namespace library.Controllers
{
    public class RegistrationController:Controller
    {
        private readonly IServiceProvider _serviceProvider;
        const string CONNECTIONSTRING = "Data Source=Catalogsdata.db";
        
        private DatabaseHelper _databaseHelper = new DatabaseHelper(CONNECTIONSTRING);
        private readonly IDataBaseHelperUser _userServices;
        ///<summary>
        ///для работы с предстваление Authorization(регистрация) вывод информации 
        /// </summary>
        public RegistrationController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userServices = _serviceProvider.GetRequiredService<IDataBaseHelperUser>();
        }
        [HttpGet]
    
        public ViewResult Authorization()
        {

            return View();
        }
        ///<summary>
        ///для работы с предстваление Authorization(регистрация) получения данных
        /// </summary>
        [HttpPost]
        public IActionResult Authorization(User user)
        {
            bool userExists = _userServices.SelectUser().Any(p => p.Login == user.Login && p.Password == user.Password && p.Admin == user.Admin);
            if (_userServices.Authorization(user, userExists))
            {
                _userServices.AddUser(user);
                return RedirectToAction("Regist", "Registration");
            }
            else
            {
                return View();
            }
          
        }
        ///<summary>
        ///для работы с предстваление Regist(авторизация) вывод информации 
        /// </summary>
        [HttpGet]
     
        public ViewResult Regist()
        {
            return View();
        }

        ///<summary>
        ///для работы с предстваление Regist(авторизация) получение информации 
        /// </summary>
        [HttpPost]
       
        public IActionResult Regist( User user)
        {
            User login = _userServices.SelectUser().FirstOrDefault(p => p.Login == user.Login && p.Password == user.Password);
            if (_userServices.Regist(user,login) == "false")
            {
                return RedirectToAction("Catalog", "Home");
            }
            if (_userServices.Regist(user,login) == "true")
            {
                return RedirectToAction("CatalogAdmin", "Home");
            }
           
      
            return View();
        }

        ///<summary>
        ///переходит к представлению Regist
        /// </summary>
        [HttpPost]
   
        public IActionResult Next()
        {
            return RedirectToAction("Regist", "Registration");
        }

        ///<summary>
        ///возвращает к представлению Authorization
        /// </summary>
        [HttpPost]
        public IActionResult Return()
      
        {
            return RedirectToAction("Authorization", "Registration");
        }

    }
}
