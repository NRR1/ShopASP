using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopASP.Application.DTO;
using ShopASP.Application.Mapping;
using ShopASP.Application.Service;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;
using ShopASP.Infrastructure.Repositories;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<GenericInterface<Role>, RoleRepository>();
builder.Services.AddScoped<GenericInterface<RoleDTO>, RoleService>();
builder.Services.AddScoped<GenericInterface<User>, UserRepository>();
builder.Services.AddScoped<GenericInterface<UserDTO>, UserService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<ShopASPDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
});

//Добавление MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
