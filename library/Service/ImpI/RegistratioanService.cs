using library.Data.Models;
using library.DataBase.Contract;
using library.Service.Contract;

namespace library.Service.ImpI
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IDataBaseHelperModels<User> _userServices;
        private readonly IServiceProvider _serviceProvider;
        public RegistrationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userServices = _serviceProvider.GetRequiredService<IDataBaseHelperModels<User>>();


        }
        public bool Authorization(User user)
        {
            if (user != null && !string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.Admin))
            {
                bool userExists = _userServices.Select().Any(p => p.Login == user.Login && p.Password == user.Password && p.Admin == user.Admin);
                if (userExists == false)
                {
                    _userServices.Insert(user);
                    return true;
                }
                else
                {

                    return false;
                }
            }
            return false;
        }

        public string Regist(User user)
        {

            if (user != null && !string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password))
            {
                User login = _userServices.Select().FirstOrDefault(p => p.Login == user.Login && p.Password == user.Password);
                if (login != null)
                {
                    if (login.Admin == "true")
                    {

                        return "true";
                    }
                    if (login.Admin == "false")
                    {

                        return "false";
                    }
                }
                return " ";
            }
            return " ";

        }
    }
}
