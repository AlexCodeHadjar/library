using Microsoft.AspNetCore.Mvc;
using library.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Policy;
using System.Diagnostics.Eventing.Reader;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using library.Service.Contract;


namespace library.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRegistrationService _businessLogicRegistration;

        ///<summary>
        ///для работы с предстваление Authorization(регистрация) вывод информации 
        /// </summary>
        public RegistrationController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _businessLogicRegistration = _serviceProvider.GetRequiredService<IRegistrationService>();
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

            if (_businessLogicRegistration.Authorization(user))
            {
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

        public IActionResult Regist(User user)
        {
            if (_businessLogicRegistration.Regist(user) == "false")
            {
                return RedirectToAction("Catalog", "Home");
            }
            if (_businessLogicRegistration.Regist(user) == "true")
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
