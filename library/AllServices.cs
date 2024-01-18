using library.BusinessLogic;
using library.DataBase;
using library.Data.Models;
using library.Service;

namespace library
{
    public class AllServices
    {
        public WebApplicationBuilder builder = WebApplication.CreateBuilder();
        public  IServiceCollection Services()
        {
            
            builder.Services.AddMvc();
            var services = builder.Services;
            // сервис для работы с базой данных для модели Author
            services.AddTransient<IDataBaseHelperModels<Author>, DatabaseHelper.DataBaseAuthor>();
            // сервис для работы с базой данных для модели Publisher
            services.AddTransient<IDataBaseHelperModels<Publisher>, DatabaseHelper.DataBasePublisher>();
            // сервис для работы с базой данных для модели BibliographicMaterial
            services.AddTransient<IDataBaseHelperModels<BibliographicMaterial>, DatabaseHelper.DataBaseBibliographicmaterial>();
            // сервис для работы с базой данных для модели User
            services.AddTransient<IDataBaseHelperModels<User>, DatabaseHelper.DataBaseUser>();
            // сервисы для работы с контролером HomeController 
            services.AddTransient<IBusinessLogicCatalog, BusenessLogicCatalog>();
            // сервисы для работы с контролером RegistrationController 
            services.AddTransient<IBusinessLogicRegistratioan, BusinessLogicRegistratioan>();

            return services;
        }
    
}
}
