using library.BusinessLogic;
using library.DataBase;
using library.Data.Models;

namespace library
{
    public class AllServices
    {
        public WebApplicationBuilder builder = WebApplication.CreateBuilder();
        public  IServiceCollection Services()
        {
            
            builder.Services.AddMvc();
            var services = builder.Services;


            //сервис для обьединения инферфейса IDataBaseHelperAuthor и класс релизации  DatabaseHelper.DataBaseAuthor
            //services.AddTransient<IDataBaseHelperAuthor, DatabaseHelper.DataBaseAuthor>();
            services.AddTransient<IDataBaseHelperModels<Author>, DatabaseHelper.DataBaseAuthor>();
            services.AddTransient<IDataBaseHelperModels<Publisher>, DatabaseHelper.DataBasePublisher>();
            services.AddTransient<IDataBaseHelperModels<BibliographicMaterial>, DatabaseHelper.DataBaseBibliographicmaterial>();
            //сервис для обьединения инферфейса IDataBaseHelperPublisher и класс релизации DatabaseHelper.DataBasePublisher
           // services.AddTransient<IDataBaseHelperPublisher, DatabaseHelper.DataBasePublisher>();

            //сервис для обьединения инферфейса IDataBaseHelperBibliographicmaterial и класс релизации DatabaseHelper.DataBaseBibliographicmaterial
            //services.AddTransient<IDataBaseHelperBibliographicmaterial, DatabaseHelper.DataBaseBibliographicmaterial>();

            //сервис для обьединения инферфейса IDataBaseHelperUser и класс релизации DatabaseHelper.DataBaseUser
            services.AddTransient<IDataBaseHelperUser, DatabaseHelper.DataBaseUser>();

            //сервис для обьединения инферфейса IDataBaseHelperLibraryCatolog и класс релизации  DatabaseHelper.DataBaseLibraryCatolog
            services.AddTransient<IBusinessLogicCatalog, BusenessLogicCatalog>();

            return services;
        }
    
}
}
