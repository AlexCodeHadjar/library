using Library.Data.interfaces;
using Library.Data.mocks;
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
app.Run();
///<summary>
/// добавляение обработчика запросов
/// </summary> 
