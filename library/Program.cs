using Microsoft.Data.Sqlite;
using library.DataBase;
using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);
 
//сервис для обьединения инферфейса IDataBaseHelperAuthor и класс релизации  DatabaseHelper.DataBaseAuthor

builder.Services.AddTransient<IDataBaseHelperAuthor, DatabaseHelper.DataBaseAuthor>();
 
//сервис для обьединения инферфейса IDataBaseHelperPublisher и класс релизации DatabaseHelper.DataBasePublisher

builder.Services.AddTransient<IDataBaseHelperPublisher, DatabaseHelper.DataBasePublisher>();
 
//сервис для обьединения инферфейса IDataBaseHelperBibliographicmaterial и класс релизации DatabaseHelper.DataBaseBibliographicmaterial

builder.Services.AddTransient<IDataBaseHelperBibliographicmaterial,DatabaseHelper.DataBaseBibliographicmaterial>();
 
//сервис для обьединения инферфейса IDataBaseHelperUser и класс релизации DatabaseHelper.DataBaseUser

builder.Services.AddTransient<IDataBaseHelperUser, DatabaseHelper.DataBaseUser>();


builder.Services.AddMvc();
var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
   name:"default",
//pattern: "{controller=Home}/{action=CatalogAdmin}");
//pattern: "{controller=Registration}/{action=Authorization}");
pattern: "{controller=Registration}/{action=Regist}");
app.Run();
