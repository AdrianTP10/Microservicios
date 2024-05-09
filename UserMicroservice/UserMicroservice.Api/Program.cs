using GymManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Policy;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using UserMicroservice.Api;
using UserMicroservice.ApplicationServices;
using UserMicroservice.Core.Users;
using UserMicroservice.DataAccess;
using UserMicroservice.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
builder.Services.AddTransient<IUsersAppService, UsersAppService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

string connectionString = builder.Configuration.GetConnectionString("Default");

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");
//string connectionString = $"server={dbHost}; port = 3306; database ={dbName}; user = root; password = {dbPassword}; CharSet = utf8";
builder.Services.AddDbContext<UserMicroserviceContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//string connectionString = builder.Configuration.GetConnectionString("Logs");
//builder.Services.AddSingleton((Serilog.ILogger)new LoggerConfiguration()
//    .WriteTo.MySQL(connectionString)
//    .CreateLogger());

//Logger 
try
{

    Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
    Log.Information("Starting up!");
    builder.Host.UseSerilog((context, services, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
    );

    Log.Information("Stopped cleanely");

}
catch (Exception e)
{
    Log.Fatal(e, "An unhandled exception occured during bootstrapping");
}
builder.Services.AddAutoMapper(typeof(MapperProfile));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<UserMicroserviceContext>();
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


app.InitDB();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
