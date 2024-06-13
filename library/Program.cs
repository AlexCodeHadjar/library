using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using library;
using DataBaseHelperSQLite;


AllServices services = new();
services.Services();

var app = services.builder.Build();


app.UseStaticFiles();

app.MapControllerRoute(
   name: "default",

//pattern: "{controller=Home}/{action=CatalogAdmin}");
pattern: "{controller=home}/{action=catalog}");

//pattern: "{controller=registration}/{action=authorization}");

//pattern: "{controller=Registration}/{action=Regist}");

app.Run();
