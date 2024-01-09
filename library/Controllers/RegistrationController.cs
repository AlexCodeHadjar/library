using Microsoft.AspNetCore.Mvc;
using library.DataBase;
using library.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Policy;
using System.Diagnostics.Eventing.Reader;
using System.Data;


namespace library.Controllers
{
    public class RegistrationController:Controller
    {
         static string connectionString = "Data Source=Catalogsdata.db";
        
        private DatabaseHelper _databaseHelper = new DatabaseHelper(connectionString);
          
        [HttpGet]
        ///<summary>
        ///для работы с предстваление Authorization(регистрация) вывод информации 
        /// </summary>
        public ViewResult Authorization()
        {

            return View();
        }
        [HttpPost]
        ///<summary>
        ///для работы с предстваление Authorization(регистрация) получения данных
        /// </summary>
        public IActionResult Authorization(User user)
        {
            if (ModelState.IsValid)
            {
               
                User newUser = _databaseHelper.SelectUser().FirstOrDefault(p => p.Login == user.Login && p.Password == user.Password && p.Admin == user.Admin);

                if (newUser == null)
                {
                    newUser = new User
                    {

                        Login = user.Login,
                        Password = user.Password,
                        Admin = user.Admin,

                    };


                  
                    // добавление пользователя(user)
                    _databaseHelper.AddUser(newUser);
                    return RedirectToAction("Regist", "Registration");
                }
            }
            return View();

        }
        [HttpGet]
        ///<summary>
        ///для работы с предстваление Regist(авторизация) вывод информации 
        /// </summary>
        public ViewResult Regist()
        {
            return View();
        }
        [HttpPost]
        ///<summary>
        ///для работы с предстваление Regist(авторизация) получение информации 
        /// </summary>
        public IActionResult Regist( User user)
        {
            if (ModelState.IsValid)
            {

               User login = _databaseHelper.SelectUser().FirstOrDefault(p => p.Login == user.Login && p.Password == user.Password && p.Admin == user.Admin);

                if (login != null)
                {
                    if (login.Admin == "false")
                    { return RedirectToAction("Catalog", "Home"); }
                    else 
                        return RedirectToAction("CatalogAdmin", "Home");
                    
                   
                }
                

            }
            return View();
        }
        [HttpPost]
        ///<summary>
        ///переходит к представлению Regist
        /// </summary>
        public IActionResult Next()
        {
            return RedirectToAction("Regist", "Registration");
        }
        [HttpPost]
        public IActionResult Return()
        ///<summary>
        ///возвращает к представлению Authorization
        /// </summary>
        {
            return RedirectToAction("Authorization", "Registration");
        }

    }
}
