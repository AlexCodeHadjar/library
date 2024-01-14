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
         const string CONNECTIONSTRING = "Data Source=Catalogsdata.db";
        
        private DatabaseHelper _databaseHelper = new DatabaseHelper(CONNECTIONSTRING);
        ///<summary>
        ///для работы с предстваление Authorization(регистрация) вывод информации 
        /// </summary>
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
            DatabaseHelper.DataBaseUser dataBaseUser = new();
            if (ModelState.IsValid)
            {
               
                User newUser = dataBaseUser.SelectUser().FirstOrDefault(p => p.Login == user.Login && p.Password == user.Password && p.Admin == user.Admin);

                if (newUser == null)
                {
                    newUser = new User
                    {

                        Login = user.Login,
                        Password = user.Password,
                        Admin = user.Admin,

                    };



                    // добавление пользователя(user)
                    dataBaseUser.AddUser(newUser);
                    return RedirectToAction("Regist", "Registration");
                }
            }
            return View();

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
            DatabaseHelper.DataBaseUser dataBaseUser = new();
            if (ModelState.IsValid)
            {

               User login = dataBaseUser.SelectUser().FirstOrDefault(p => p.Login == user.Login && p.Password == user.Password );

                if (login != null)
                {
                    if (login.Admin== "false")
                    { return RedirectToAction("Catalog", "Home"); }
                    else 
                        return RedirectToAction("CatalogAdmin", "Home");
                    
                   
                }
                

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
