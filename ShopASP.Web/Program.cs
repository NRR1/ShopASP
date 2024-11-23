using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopASP.Application.Interfaces;
using ShopASP.Application.Mapping;
using ShopASP.Application.Services;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;
using ShopASP.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args); //Создаётся объект builder, который используется для конфигурации и подготовки приложения перед запуском

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ShopASPDBContext>()
    .AddDefaultTokenProviders();



// Add services to the container.
builder.Services.AddControllersWithViews();                         //Добавляет в приложение шаблон MVC
builder.Services.AddAutoMapper(typeof(MappingProfile));             //Добавляет Mapper в контейнер DI

//builder.Services.AddScoped<IUserRepository, UserRepository>();      //Указывается внедрение зависимостей
//builder.Services.AddScoped<ILogin<User>, LoginRepository>();        //То есть в параметре 2 всегда будет использоваться
//builder.Services.AddScoped<IProductReposutory, ProductRepository>();//параметр 1
//builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<ShopASPDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
});

//Эта конструкция используется для регистрации контекста БД EFCore в DI контейнере с использованием Microsoft.SqlServer
//в качестве базы данных

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.Cookie.Name = "UserAuthCookie";
    });

//Добавление структуры MVC(Model, View, Controller)
builder.Services.AddControllersWithViews();

var app = builder.Build();//Переходит от этапа настройки приложения(builder) к созданию готового экземпляра

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
    
app.UseHttpsRedirection(); //Автоматически перенаправляет с HTTP на HTTPS(безопасное соединение между клиентом и сервером, шифруя данные)
app.UseStaticFiles(); //Подключает в приложение такие файлы как CSS, JS, изображения, файлы шрифтов

app.UseRouting(); //Добавляет в приложение механизм маршрутизации
app.UseAuthentication();//Добавляет в приложение возможность аутентификации
app.UseAuthorization();//Добавляет в приложение возможность авторизации

app.MapControllerRoute(//задаёт маршрут для MVC в ASP.NET Core
    name: "default",//Имя маршрута, в данном случае default - уникальный идентификатор
    pattern: "{controller=Product}/{action=Index}/{id?}");//Первая часть - имя контроллера, вторая часть - метод в контроллере, третья - например ID строки в бд(ID пользователя), может быть null
/* MapControllerRoute используется для настройки маршрутизации в приложении
 * Это позволяет сопоставить URL-запросы с определенными контроллерами, методами и их параметрами
 */

app.Run();//Запуск приложения

/*
 Каждый день я плачу и думаю
 Как тебя хуячу
 */