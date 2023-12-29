using Library.Data.interfaces;
using Library.Data.mocks;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using library.DataBase;
using library.Data.Models;
var builder = WebApplication.CreateBuilder(args);
///<summary>
///�������� ���������� ������� WebApplication
/// </summary>�

builder.Services.AddTransient<IAuthor, MockAuthor>();
///<summary>
///������ ��� ����������� ���������� IAuthor � ����� ��������� MockAuthor
/// </summary>
builder.Services.AddTransient<IPublicsher, MockPublicsher>();
///<summary>
///������ ��� ����������� ���������� IAuthor � ����� ��������� MockAuthor
/// </summary>
builder.Services.AddTransient<IBibliographicmaterial, MockBibliographicmaterial>();
///<summary>
///������ ��� ����������� ���������� IAuthor � ����� ��������� MockAuthor
/// </summary>
builder.Services.AddMvc();
///<summary>
/// ��������� ������� MVC
/// </summary
var app = builder.Build();
///<summary>
/// ����������������� ���-����������
/// </summary>
app.UseStaticFiles();
///<summary>
/// ������������ ������� � ����������� ������
/// </summary>
app.MapControllerRoute(
   name:"default",
   pattern: "{controller=Home}/{action=Catalog}");
///<summary>
/// ������� ��� ��������� �������� � ��������� ������������
/// </summary> 
string connectionString = "Data Source=Catalogsdata.db";
///<summary>
/// ������ �����������
/// </summary> 




DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);
///<summary>
/// �������� ������ �����������
/// </summary> 

Bibliographicmaterial newMaterial = new Bibliographicmaterial()
{
    name = "�������� �����",
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
/// ����������� ������ Bibliographicmaterial � �������
/// </summary> 
Author newAuthor = new Author()
{
    fullName = "���� ������",
    contacts = "ivan@example.com",
    information = "���������� �� ������"
};

databaseHelper.AddAuthor(newAuthor);
///<summary>
/// ����������� ������ Author � �������
/// </summary> 

Publisher newPublisher = new Publisher()
{
    name = "������������",
    contacts = "publisher@example.com",
    address = "����� ������������"
};

databaseHelper.AddPublisher(newPublisher);
///<summary>
/// ����������� ������ Publisher � �������
/// </summary> 

app.Run();
///<summary>
/// ����������� ����������� ��������
/// </summary> 
