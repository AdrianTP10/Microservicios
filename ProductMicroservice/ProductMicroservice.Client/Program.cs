using ProductMicroservice.AplicationServices;
using ProductMicroservice.AplicationServices.Navigation;
using ProductMicroservice.ApplicationServices.Products;
using ProductMicroservice.Core.Products;
using ProductMicroservice.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddTransient<IMenuAppService, MenuAppService>();

builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(MapperProfile));

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
