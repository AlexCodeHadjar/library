using Library.Data.interfaces;
using Library.Data.mocks;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using library.DataBase;
using library.Data.Models;
var builder = WebApplication.CreateBuilder(args);
///<summary>
///создание экземпляра объекта WebApplication
/// </summary>с

builder.Services.AddTransient<IAuthor, MockAuthor>();
///<summary>
///сервис для обьединения инферфейса IAuthor и класс релизации MockAuthor
/// </summary>
builder.Services.AddTransient<IPublicsher, MockPublicsher>();
///<summary>
///сервис для обьединения инферфейса IAuthor и класс релизации MockAuthor
/// </summary>
builder.Services.AddTransient<IBibliographicmaterial, MockBibliographicmaterial>();
///<summary>
///сервис для обьединения инферфейса IAuthor и класс релизации MockAuthor
/// </summary>
builder.Services.AddMvc();
///<summary>
/// добавляет сервисы MVC
/// </summary
var app = builder.Build();
///<summary>
/// конфигурированное веб-приложение
/// </summary>
app.UseStaticFiles();
///<summary>
/// обрабатывает запросы к статическим файлам
/// </summary>
app.MapControllerRoute(
   name:"default",
   pattern: "{controller=Home}/{action=Catalog}");
///<summary>
/// маршрут для обработки запросов к действиям контроллеров
/// </summary> 
string connectionString = "Data Source=Catalogsdata.db";
///<summary>
/// строка подключения
/// </summary> 




DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
///<summary>
/// передача строки подключения
/// </summary> 

Bibliographicmaterial newMaterial = new Bibliographicmaterial()
{
    name = "Название книги",
    date = "2022-01-01",
    img = "book.jpg",
    author = new Author()
    {
        id = 1
    },
    publisher = new Publisher()
    {
        id = 1
    }
};

databaseHelper.AddBibliographicMaterial(newMaterial);
///<summary>
/// добавляение нового Bibliographicmaterial в таблицу
/// </summary> 
Author newAuthor = new Author()
{
    fullName = "Иван Иванов",
    contacts = "ivan@example.com",
    information = "Информация об авторе"
};

databaseHelper.AddAuthor(newAuthor);
///<summary>
/// добавляение нового Author в таблицу
/// </summary> 

Publisher newPublisher = new Publisher()
{
    name = "Издательство",
    contacts = "publisher@example.com",
    address = "Адрес издательства"
};

databaseHelper.AddPublisher(newPublisher);
///<summary>
/// добавляение нового Publisher в таблицу
/// </summary> 

app.Run();
///<summary>
/// добавляение обработчика запросов
/// </summary> 
