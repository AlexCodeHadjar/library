using Microsoft.Data.Sqlite;
using library.DataBase;
using library.Data.Models;
using library.Data.interfaces;
using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);
///<summary>
///������ ��� ����������� ���������� IDataBaseHelperAuthor � ����� ���������  DatabaseHelper.DataBaseAuthor
/// </summary>
builder.Services.AddTransient<IDataBaseHelperAuthor, DatabaseHelper.DataBaseAuthor>();
///<summary>
///������ ��� ����������� ���������� IDataBaseHelperPublisher � ����� ��������� DatabaseHelper.DataBasePublisher
/// </summary>
builder.Services.AddTransient<IDataBaseHelperPublisher, DatabaseHelper.DataBasePublisher>();
///<summary>
///������ ��� ����������� ���������� IDataBaseHelperBibliographicmaterial � ����� ��������� DatabaseHelper.DataBaseBibliographicmaterial
/// </summary>
builder.Services.AddTransient<IDataBaseHelperBibliographicmaterial,DatabaseHelper.DataBaseBibliographicmaterial>();
///<summary>
///������ ��� ����������� ���������� IDataBaseHelperUser � ����� ��������� DatabaseHelper.DataBaseUser
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
