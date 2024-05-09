using Microsoft.EntityFrameworkCore;
using OrderMicroservice.ApplicationServices;
using OrderMicroservice.ApplicationServices.Cities;
using OrderMicroservice.ApplicationServices.Orders;
using OrderMicroservice.ApplicationServices.Products;
using OrderMicroservice.ApplicationServices.Users;
using OrderMicroservice.Core.Orders;
using OrderMicroservice.Core.Products;
using OrderMicroservice.Core.Users;
using OrderMicroservice.DataAccess;
using OrderMicroservice.DataAccess.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<OrderMicroserviceContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddTransient<IOrdersAppServices, OrderAppServices>();
builder.Services.AddTransient<IUserAppServices, UserAppServices>();
builder.Services.AddTransient<IProductAppServices, ProductAppServices>();
builder.Services.AddScoped<ICityAppServices, CityAppServices>();
builder.Services.AddTransient<IRepository<int, Order>, Repository<int, Order>>();
builder.Services.AddTransient<IRepository<int, Product>, Repository<int, Product>>();
builder.Services.AddTransient<IRepository<int, User>, Repository<int, User>>();
builder.Services.AddScoped<IRepository<int, City>, Repository<int, City>>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuración del Serilog
Log.Logger = new LoggerConfiguration().WriteTo.MySQL(connectionString, "Logs").CreateLogger();
// Registro de evento de inicio
Log.Information("The OrderMicroservice application has started.");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
