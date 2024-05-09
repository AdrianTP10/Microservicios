using Microsoft.EntityFrameworkCore;
using ProductMicroservice.ApplicationServices.Products;
using ProductMicroservice.Core.Products;
using ProductMicroservice.DataAccess;
using ProductMicroservice.DataAccess.Repositories;

namespace ProductMicroservice.Test
{
    public class UnitTest1
    {
        public readonly DbContextOptions<ProductMicroserviceContext> dbContextOptions;

        public UnitTest1()
        {
            // Build DbContextOptions
            dbContextOptions = new DbContextOptionsBuilder<ProductMicroserviceContext>()
                .UseInMemoryDatabase(databaseName: "DataTest")
                .Options;
        }

        [Fact]
        public async Task Test1Async()
        {
            //Obtiene el contexto
            var dbContext = new ProductMicroserviceContext(dbContextOptions);
            //Crea el repositorio en base al contexto
            IRepository<int, Product> repository = new Repository<int, Product>(dbContext);
            //Crea el servicio
            /*
            ProductAppServices service = new ProductAppServices(repository);

            Product product1 = new Product
            {
                Id = 1,
                Name = "Takis Fuego",
                Description = "Fritura con chile",
                Brand = "Barcel",
                Category = "Fritura",
                Price = 10.5
            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "Churrumais",
                Description = "Fritura con chile y limón",
                Brand = "Sabritas",
                Category = "Fritura",
                Price = 9.5
            };
            Product product3 = new Product
            {
                Id = 3,
                Name = "Chokies",
                Description = "Galletas con shispas de chocolate",
                Brand = "Marinela",
                Category = "Galletas",
                Price = 14.5
            };

            service.AddProductAsync(product1);
            service.AddProductAsync(product2);
            service.AddProductAsync(product3);

            //Obtiene el listado de miembros
            List<Product> list = await service.GetProductsAsync();
            Assert.NotEmpty(list);
            */
        }
    }
}