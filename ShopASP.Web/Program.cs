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

//����� ������ program.cs

//��������� Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ShopASPDBContext>()
    .AddDefaultTokenProviders();

//���������� ����������� � ��������������
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

//���������� MVC
builder.Services.AddControllersWithViews();









//����� �������

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//������ �������


//��������� �������������� � �����������
app.UseAuthentication();
app.UseAuthorization();

//�������� ����� � ��������������� ��� ������ ������� ����������
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    //����������� �����
    string[] rolenames = { "Admin", "User" };
    IdentityResult roleResult;

    //�������� ������� ����� � �� �������� ��� �������������
    foreach (var roleName in rolenames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    //�������� ��������������� �� ������
    var adminLogin = "Admin";
    var adminUser = await userManager.FindByNameAsync(adminLogin);

    //���� ��������������� ���, ������ ��� � ��������� � ���� Admin
    if(adminUser == null)
    {
        adminUser = new IdentityUser { UserName = adminLogin };
        string adminPassword = "Admin";

        var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
        if (createAdmin.Succeeded)
        {
            //��������� ��������������� � ���� Admin
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
    else
    {
        //���� �������������� ��� ����������, ��������� ��� ����
        var isAdmin = await userManager.IsInRoleAsync(adminUser, "Admin");
        if (!isAdmin)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

//����� �������

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
