using DataBaseHelperSQLite.Data.Models;
using DataBaseHelperSQLite.DataBase.Contract;
using DataBaseHelperSQLite.DataBase.Impl;
using library.Service.Contract;
using library.Service.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace XUnitTest
{
    public class MockedServiceCollection
    {

        const string CONNECTION_STRING = "Data Source=../../../../Catalogsdata.db";
        //const string CONNECTION_STRING = "Data Source=C:\\Users\\user\\source\\repos\\library\\Catalogsdata.db";
        public WebApplicationBuilder builder = WebApplication.CreateBuilder();
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        private readonly IServiceProvider _serviceProvider ;
        public MockedServiceCollection()
        {
            AddService(typeof(IDataBaseHelperModels<Author>), new DataBaseAuthor(CONNECTION_STRING));
            AddService(typeof(IDataBaseHelperModels<Publisher>), new DataBasePublisher(CONNECTION_STRING));
            AddService(typeof(IDataBaseHelperModels<BibliographicMaterial>), new DataBaseBibliographicmaterial(CONNECTION_STRING));
            AddService(typeof(IDataBaseHelperModels<User>), new DataBaseUser(CONNECTION_STRING));
            

            var serviceCollection = new ServiceCollection();
            foreach (var serviceEntry in _services)
            {
                serviceCollection.AddSingleton(serviceEntry.Key, serviceEntry.Value);
            }
            serviceCollection.AddTransient<ICatalogService, CatalogService>();

         
            serviceCollection.AddTransient<IRegistrationService, RegistrationService>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public void AddService(Type t, object o)
        {
            if (_services.ContainsKey(t))
            {
                throw new Exception($"Инстанс для {t.Name} уже добавлен");
            }
            _services.Add(t, o);
        }
        public object GetService(Type serviceType)
        {
            if (!_services.ContainsKey(serviceType))
            {
                throw new Exception($"Инстанс для {serviceType.Name} не найден");
            }
            return _services[serviceType];
        }
        public T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }




    }
}
