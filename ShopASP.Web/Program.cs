using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopASP.Application.Interface;
using ShopASP.Application.Mapping;
using ShopASP.Application.Services;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;
using ShopASP.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Privacy";
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ShopASPDBContext>()
    .AddDefaultTokenProviders();    
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;               //�������� ���� �� 1 �����+
    options.Password.RequiredLength = 8;                //����������� ������ ������ +
    options.Password.RequireNonAlphanumeric = true;     //������� ���������� +
    options.Password.RequireUppercase = true;           //������� ��������� �����+
    options.Password.RequireLowercase = true;           //������� �������� �����+
    options.Password.RequiredUniqueChars = 1;           //������� ���������� ������+
    //������ ���������� ������: @vdoshka931MMM


    //����� ��������������� ���:
    /*
    UserName = admin@mail.ru
    Email = admin@mail.ru
    Password = @dminPassword667

    ��� ��� ������ ��������� � ����� DbInitializer
     */
});

builder.Services.AddDbContext<ShopASPDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DbInitializer.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "������ ��� ������������� ��");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
    
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();