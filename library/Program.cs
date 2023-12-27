using Library.Data.interfaces;
using Library.Data.mocks;
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
app.Run();
///<summary>
/// ����������� ����������� ��������
/// </summary> 
