using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductMicroservice.Core.Products;
using ProductMicroservice.DataAccess.Repositories;
using ProductMicroservice.ApplicationServices.Products;
using ProductMicroservice.DataAccess;
using ProductMicroservice.AplicationServices;

namespace ProductMicroservice.NUnitTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ProductMicroserviceContext>(options => options.UseInMemoryDatabase(databaseName: "DataTest"));
            services.AddTransient<IProductAppServices, ProductAppServices>();
            services.AddTransient<IRepository<int, Product>, Repository<int, Product>>();

            services.AddAutoMapper(typeof(MapperProfile));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
