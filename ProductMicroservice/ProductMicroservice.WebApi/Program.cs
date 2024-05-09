using Microsoft.EntityFrameworkCore;
using ProductMicroservice.AplicationServices;
using ProductMicroservice.ApplicationServices.Products;
using ProductMicroservice.Core.Products;
using ProductMicroservice.DataAccess;
using ProductMicroservice.DataAccess.Repositories;
using Microsoft.Extensions.Http;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//string connectionString = builder.Configuration.GetConnectionString("Default");

var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
var dbname = Environment.GetEnvironmentVariable("DB_NAME");
var dbpassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");
string connectionString = $"server={dbhost}; port = 3306; database ={dbname}; user = root; password = {dbpassword}; charset = utf8";

builder.Services.AddDbContext<ProductMicroserviceContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddTransient<IProductAppServices, ProductAppServices>();
builder.Services.AddTransient<IRepository<int, Product>, Repository<int, Product>>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuración del Serilog
Log.Logger = new LoggerConfiguration().WriteTo.MySQL(connectionString, "Logs").CreateLogger();
// Registro de evento de inicio
Log.Information("The ProductMicroservice application has started.");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ProductMicroserviceContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}
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
