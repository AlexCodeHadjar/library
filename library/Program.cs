using Microsoft.Data.Sqlite;
using library.DataBase;
using library.Data.Models;
using library.Data.interfaces;
using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);
///<summary>
///сервис для обьединения инферфейса IDataBaseHelperAuthor и класс релизации  DatabaseHelper.DataBaseAuthor
/// </summary>
builder.Services.AddTransient<IDataBaseHelperAuthor, DatabaseHelper.DataBaseAuthor>();
///<summary>
///сервис для обьединения инферфейса IDataBaseHelperPublisher и класс релизации DatabaseHelper.DataBasePublisher
/// </summary>
builder.Services.AddTransient<IDataBaseHelperPublisher, DatabaseHelper.DataBasePublisher>();
///<summary>
///сервис для обьединения инферфейса IDataBaseHelperBibliographicmaterial и класс релизации DatabaseHelper.DataBaseBibliographicmaterial
/// </summary>
builder.Services.AddTransient<IDataBaseHelperBibliographicmaterial,DatabaseHelper.DataBaseBibliographicmaterial>();
///<summary>
///сервис для обьединения инферфейса IDataBaseHelperUser и класс релизации DatabaseHelper.DataBaseUser
/// </summary>
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
