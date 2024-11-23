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

var builder = WebApplication.CreateBuilder(args); //�������� ������ builder, ������� ������������ ��� ������������ � ���������� ���������� ����� ��������

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ShopASPDBContext>()
    .AddDefaultTokenProviders();



// Add services to the container.
builder.Services.AddControllersWithViews();                         //��������� � ���������� ������ MVC
builder.Services.AddAutoMapper(typeof(MappingProfile));             //��������� Mapper � ��������� DI

//builder.Services.AddScoped<IUserRepository, UserRepository>();      //����������� ��������� ������������
//builder.Services.AddScoped<ILogin<User>, LoginRepository>();        //�� ���� � ��������� 2 ������ ����� ��������������
//builder.Services.AddScoped<IProductReposutory, ProductRepository>();//�������� 1
//builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<ShopASPDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
});

//��� ����������� ������������ ��� ����������� ��������� �� EFCore � DI ���������� � �������������� Microsoft.SqlServer
//� �������� ���� ������

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.Cookie.Name = "UserAuthCookie";
    });

//���������� ��������� MVC(Model, View, Controller)
builder.Services.AddControllersWithViews();

var app = builder.Build();//��������� �� ����� ��������� ����������(builder) � �������� �������� ����������

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
    
app.UseHttpsRedirection(); //������������� �������������� � HTTP �� HTTPS(���������� ���������� ����� �������� � ��������, ������ ������)
app.UseStaticFiles(); //���������� � ���������� ����� ����� ��� CSS, JS, �����������, ����� �������

app.UseRouting(); //��������� � ���������� �������� �������������
app.UseAuthentication();//��������� � ���������� ����������� ��������������
app.UseAuthorization();//��������� � ���������� ����������� �����������

app.MapControllerRoute(//����� ������� ��� MVC � ASP.NET Core
    name: "default",//��� ��������, � ������ ������ default - ���������� �������������
    pattern: "{controller=Product}/{action=Index}/{id?}");//������ ����� - ��� �����������, ������ ����� - ����� � �����������, ������ - �������� ID ������ � ��(ID ������������), ����� ���� null
/* MapControllerRoute ������������ ��� ��������� ������������� � ����������
 * ��� ��������� ����������� URL-������� � ������������� �������������, �������� � �� �����������
 */

app.Run();//������ ����������

/*
 ������ ���� � ����� � �����
 ��� ���� �����
 */