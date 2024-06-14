using DataBaseHelperSQLite.Data.Models;
using library.Service.Impl;
using library.Service.Contract;
using DataBaseHelperSQLite.DataBase.Contract;
using DataBaseHelperSQLite.DataBase.Impl;
using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace library
{
    public class AllServices
    {
        //public const string CONNECTION_STRING = "Data Source=C:\\Users\\user\\source\\repos\\library\\Catalogsdata.db";
        public const string CONNECTION_STRING = "Data Source=../Catalogsdata.db";
        public WebApplicationBuilder builder = WebApplication.CreateBuilder();
        public IServiceCollection Services()
        {

         
            //const string CONNECTION_STRING = "Data Source=C:\\Users\\user\\source\\repos\\library\\Catalogsdata.db";
                    

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
