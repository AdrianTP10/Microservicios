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
using OrderMicroservice.DataAccess;
using OrderMicroservice.ApplicationServices.Orders;
using OrderMicroservice.DataAccess.Repositories;
using OrderMicroservice.Core.Orders;
using OrderMicroservice.ApplicationServices;

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
            services.AddDbContext<OrderMicroserviceContext>(options => options.UseInMemoryDatabase(databaseName: "DataTest"));
            services.AddTransient<IOrdersAppServices, OrderAppServices>();
            services.AddTransient<IRepository<int, Order>, Repository<int, Order>>();

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
