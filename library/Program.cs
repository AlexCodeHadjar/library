using Library.Data.interfaces;
using Library.Data.mocks;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using library.DataBase;
using library.Data.Models;
var builder = WebApplication.CreateBuilder(args);
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
   pattern: "{controller=Home}/{action=Catalog}");
// строка подключения
string connectionString = "Data Source=Catalogsdata.db";






// передача строки подключения
DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);


Bibliographicmaterial newMaterial = new Bibliographicmaterial()
{
    Name = "Название книг",
    Date = "2022-01-01",
    Img = "book.jpg",
    Author = new Author()
    {
        Id = 1
    },
    Publisher = new Publisher()
    {
        Id = 1
    }
};
// добавляение нового Bibliographicmaterial в таблицу
databaseHelper.AddBibliographicMaterial(newMaterial);

Author newAuthor = new Author()
{
    FullName = "Иван Иванов",
    Contacts = "ivan@example.com",
    Information = "Информация об авторе"
};
// добавляение нового Author в таблицу
databaseHelper.AddAuthor(newAuthor);


Publisher newPublisher = new Publisher()
{
    Name = "Издательство",
    Contacts = "publisher@example.com",
    Address = "Адрес издательства"
};

// добавляение нового Publisher в таблицу
databaseHelper.AddPublisher(newPublisher);



//var obj = databaseHelper.SelectBibliographicmaterial();

//databaseHelper.SelectPublisher();
//databaseHelper.SelectAuthor();
//Console.Read();
app.Run();
