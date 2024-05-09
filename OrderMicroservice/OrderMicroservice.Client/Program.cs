using Microsoft.EntityFrameworkCore;
using OrderMicroservice.AplicationServices.Navigation;
using OrderMicroservice.ApplicationServices;
using OrderMicroservice.ApplicationServices.Cities;
using OrderMicroservice.Core.Users;
using OrderMicroservice.DataAccess;
using OrderMicroservice.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
string connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<OrderMicroserviceContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddTransient<IMenuAppService, MenuAppService>();
builder.Services.AddScoped<ICityAppServices, CityAppServices>();
builder.Services.AddScoped<IRepository<int, City>, Repository<int, City>>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
