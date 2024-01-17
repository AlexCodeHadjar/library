using Microsoft.Data.Sqlite;
using library.DataBase;
using System.Data.SQLite;
using library;

AllServices services = new();
services.Services();


var app = services.builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
   name:"default",
//pattern: "{controller=Home}/{action=CatalogAdmin}");
//pattern: "{controller=Registration}/{action=Authorization}");
pattern: "{controller=Registration}/{action=Regist}");
app.Run();
