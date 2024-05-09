using Microsoft.AspNetCore.Identity;
using UserMicroservice.Core.Users;
using UserMicroservice.DataAccess;

namespace UserMicroservice.Api
{
    public static class InitDbExtensions
    {
        public static IApplicationBuilder InitDB(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var contextManager = services.GetService<UserMicroserviceContext>();

                if (contextManager.Cities.Count() == 0)
                {
                    Task.Run(() => InitPayments(contextManager)).Wait();
                }

                return app;
            }
        }
        private static async Task InitPayments(UserMicroserviceContext contextManager)
        {
            var cities = new List<City>
            {
               new City { Id = 1, Name= "México City"},
               new City { Id = 2, Name= "Tijuana"},
               new City { Id = 3, Name= "Querétaro"},
               new City { Id = 4, Name= "Monterrey"},
               new City { Id = 5, Name= "Guadalajara"},
            };

            await contextManager.AddRangeAsync(cities);
            await contextManager.SaveChangesAsync();    
        }

    }
}

