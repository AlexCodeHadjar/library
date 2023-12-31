using Microsoft.AspNetCore.Mvc;
using library.DataBase;
using library.Data.Models;
using Microsoft.AspNetCore.Identity;


namespace library.Controllers
{
    ///<summary>
    ///контролер для работы с предстваление Regist 
    /// </summary>
    public class RegistrationController:Controller
    {
         static string connectionString = "Data Source=Catalogsdata.db";
        private DatabaseHelper _databaseHelper = new DatabaseHelper(connectionString);
            
        [HttpGet]
        ///<summary>
        ///для работы с предстваление CataRegistlog вывод информации 
        /// </summary>
        public ViewResult Regist()
        {

            return View();
        }
        [HttpPost]
        ///<summary>
        ///для работы с предстваление CataRegistlog получения данных
        /// </summary>
        public IActionResult Regist(User user)
        {
            if (ModelState.IsValid)
            {

                User newUser = new User
                {
                    Login = user.Login,
                    Password = user.Password,
                    Admin = user.Admin,
                    
                };
                // добавление пользователя(user)
                _databaseHelper.AddUser(newUser);
                return RedirectToAction("Catalog", "Home");
            }
            return View();
        }
    }
}
