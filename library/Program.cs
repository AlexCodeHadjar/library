using Microsoft.Data.Sqlite;
using library.DataBase;
using library.Data.Models;
using library.Data.interfaces;
using library.Data.mocks;
using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);
///<summary>
///сервис для обьединения инферфейса IUser и класс релизации MockUser
/// </summary>
builder.Services.AddTransient<IUser, MockUser>();
///<summary>
///сервис для обьединения инферфейса IAuthor и класс релизации MockAuthor
/// </summary>
builder.Services.AddTransient<IAuthor, MockAuthor>();
///<summary>
///сервис для обьединения инферфейса IAuthor и класс релизации MockAuthor
/// </summary>
builder.Services.AddTransient<IPublicsher, MockPublicsher>();
///<summary>
///сервис для обьединения инферфейса IAuthor и класс релизации MockAuthor
/// </summary>
builder.Services.AddTransient<IBibliographicmaterial, MockBibliographicmaterial>();


builder.Services.AddMvc();
var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
   name:"default",
//pattern: "{controller=Home}/{action=CatalogAdmin}");
//pattern: "{controller=Registration}/{action=Authorization}");
pattern: "{controller=Registration}/{action=Regist}");
app.Run();
