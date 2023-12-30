using Library.Data.interfaces;
using Library.Data.mocks;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using library.DataBase;
using library.Data.Models;
var builder = WebApplication.CreateBuilder(args);
///<summary>
///������ ��� ����������� ���������� IAuthor � ����� ��������� MockAuthor
/// </summary>
builder.Services.AddTransient<IAuthor, MockAuthor>();
///<summary>
///������ ��� ����������� ���������� IAuthor � ����� ��������� MockAuthor
/// </summary>
builder.Services.AddTransient<IPublicsher, MockPublicsher>();
///<summary>
///������ ��� ����������� ���������� IAuthor � ����� ��������� MockAuthor
/// </summary>
builder.Services.AddTransient<IBibliographicmaterial, MockBibliographicmaterial>();

builder.Services.AddMvc();
var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
   name:"default",
   pattern: "{controller=Home}/{action=Catalog}");
// ������ �����������
string connectionString = "Data Source=Catalogsdata.db";






// �������� ������ �����������
DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);


Bibliographicmaterial newMaterial = new Bibliographicmaterial()
{
    Name = "�������� ����",
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
// ����������� ������ Bibliographicmaterial � �������
databaseHelper.AddBibliographicMaterial(newMaterial);

Author newAuthor = new Author()
{
    FullName = "���� ������",
    Contacts = "ivan@example.com",
    Information = "���������� �� ������"
};
// ����������� ������ Author � �������
databaseHelper.AddAuthor(newAuthor);


Publisher newPublisher = new Publisher()
{
    Name = "������������",
    Contacts = "publisher@example.com",
    Address = "����� ������������"
};

// ����������� ������ Publisher � �������
databaseHelper.AddPublisher(newPublisher);



//var obj = databaseHelper.SelectBibliographicmaterial();

//databaseHelper.SelectPublisher();
//databaseHelper.SelectAuthor();
//Console.Read();
app.Run();
