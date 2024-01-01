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
   pattern: "{controller=Registration}/{action=Authorization}");
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
//databaseHelper.AddBibliographicMaterial(newMaterial);


Author newAuthor = new Author()
{
    FullName = "Иван Иванов",
    Contacts = "ivan@example.com",
    Information = "Информация об авторе"
};
// добавляение нового Author в таблицу
//databaseHelper.AddAuthor(newAuthor);



Publisher newPublisher = new Publisher()
{
    Name = "Издательство",
    Contacts = "publisher@example.com",
    Address = "Адрес издательства"
};

// добавляение нового Publisher в таблицу
//databaseHelper.AddPublisher(newPublisher);
/*using (SQLiteConnection connection = new SQLiteConnection(connectionString))
{
    connection.Open();

    string sqlExpression = "CREATE TABLE IF NOT EXISTS User (id INTEGER PRIMARY KEY, login TEXT, password TEXT,Admin TEXT )";
    using (SQLiteCommand command = new SQLiteCommand(sqlExpression, connection))
    {
        command.ExecuteNonQuery();
    }

    // Закрытие соединения
    connection.Close();
}*/


//var obj = databaseHelper.SelectBibliographicmaterial();

//databaseHelper.SelectPublisher();
//databaseHelper.SelectAuthor();
//Console.Read();
app.Run();
