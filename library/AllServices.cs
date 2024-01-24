using library.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using static library.DataBase.ImpI.DatabaseHelper;
using library.Service.ImpI;
using library.Service.Contract;
using library.DataBase.Contract;
using library.DataBase.ImpI;


namespace library
{
    public class AllServices
    {
        public WebApplicationBuilder builder = WebApplication.CreateBuilder();
        public IServiceCollection Services()
        {

            const string CONNECTION_STRING = "Data Source=Catalogsdata.db";

            builder.Services.AddMvc();
            var services = builder.Services;

            // сервис для работы с базой данных для модели Author
            services.AddTransient<IDataBaseHelperModels<Author>>(provider => new DataBaseAuthor(CONNECTION_STRING));

            // сервис для работы с базой данных для модели Publisher
            services.AddTransient<IDataBaseHelperModels<Publisher>>(provider => new DataBasePublisher(CONNECTION_STRING));

            // сервис для работы с базой данных для модели BibliographicMaterial
            services.AddTransient<IDataBaseHelperModels<BibliographicMaterial>>(provider => new DataBaseBibliographicmaterial(CONNECTION_STRING));

            // сервис для работы с базой данных для модели User
            services.AddTransient<IDataBaseHelperModels<User>>(provider => new DataBaseUser(CONNECTION_STRING));

            // сервисы для работы с контролером HomeController 
            services.AddTransient<ICatalogService, CatalogService>();

            // сервисы для работы с контролером RegistrationController 
            services.AddTransient<IRegistrationService, RegistrationService>();

            return services;
        }

    }
}
