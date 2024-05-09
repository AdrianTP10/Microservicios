using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductMicroservice.AplicationServices;
using ProductMicroservice.ApplicationServices.Products;
using ProductMicroservice.Core.Products;
using ProductMicroservice.Core.ProductsDTO;
using ProductMicroservice.DataAccess;

namespace ProductMicroservice.NUnitTest
{
    [TestFixture]
    public class ProductTest
    {
        protected TestServer _server;
        private IMapper _mapper;

        [OneTimeSetUp]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            _mapper = configuration.CreateMapper();
        }

        [Test]
        public async Task AddProductAsync()
        {
            var repository = _server.Host.Services.GetService<IProductAppServices>();
            //Agregar productos al servicio
            ProductDTO product1 = new ProductDTO
            {
                Id = 1,
                Name = "Takis Fuego",
                WrapperType = "Bolsa de aluminio",
                DescriptionProduct = "Fritura con chile",
                Brand = "Barcel",
                CategoryProduct = "Fritura",
                Price = 10,
                FinalPrice = 11.5
            };

            int id = await repository.AddProductAsync(product1);
            //Listar el contenido
            List<Product> list = await repository.GetProductsAsync();
            Assert.Pass();
        }

        [Test]
        public async Task GetProductsAsync()
        {
            var repository = _server.Host.Services.GetService<IProductAppServices>();
            //Agregar productos al servicio
            ProductDTO product1 = new ProductDTO
            {
                Id = 1,
                Name = "Takis Fuego",
                WrapperType = "Bolsa de aluminio",
                DescriptionProduct = "Fritura con chile",
                Brand = "Barcel",
                CategoryProduct = "Fritura",
                Price = 10,
                FinalPrice = 11.5
            };
            ProductDTO product2 = new ProductDTO
            {
                Id = 2,
                Name = "Churrumais",
                WrapperType = "Bolsa normal",
                DescriptionProduct = "Fritura con chile y limón",
                Brand = "Sabritas",
                CategoryProduct = "Fritura",
                Price = 9.5,
                FinalPrice = 10.5
            };
            ProductDTO product3 = new ProductDTO
            {
                Id = 3,
                Name = "Chokies",
                WrapperType = "Envoltura de plastico",
                DescriptionProduct = "Galletas con shispas de chocolate",
                Brand = "Marinela",
                CategoryProduct = "Galletas",
                Price = 14.5,
                FinalPrice = 10.5
            };

            await repository.AddProductAsync(product1);
            await repository.AddProductAsync(product2);
            await repository.AddProductAsync(product3);
            //Listar el contenido
            List<Product> list = await repository.GetProductsAsync();
            Assert.Pass();
        }

        [Test]
        public async Task GetProductAsync()
        {
            var repository = _server.Host.Services.GetService<IProductAppServices>();
            //Agregar productos al servicio
            ProductDTO product1 = new ProductDTO
            {
                Id = 1,
                Name = "Takis Fuego",
                WrapperType = "Bolsa de aluminio",
                DescriptionProduct = "Fritura con chile",
                Brand = "Barcel",
                CategoryProduct = "Fritura",
                Price = 10,
                FinalPrice = 11.5
            };
            ProductDTO product2 = new ProductDTO
            {
                Id = 2,
                Name = "Churrumais",
                WrapperType = "Bolsa normal",
                DescriptionProduct = "Fritura con chile y limón",
                Brand = "Sabritas",
                CategoryProduct = "Fritura",
                Price = 9.5,
                FinalPrice = 10.5
            };
            ProductDTO product3 = new ProductDTO
            {
                Id = 3,
                Name = "Chokies",
                WrapperType = "Envoltura de plastico",
                DescriptionProduct = "Galletas con shispas de chocolate",
                Brand = "Marinela",
                CategoryProduct = "Galletas",
                Price = 14.5,
                FinalPrice = 10.5
            };

            await repository.AddProductAsync(product1);
            await repository.AddProductAsync(product2);
            await repository.AddProductAsync(product3);
            //Consultar uno en especifico
            Product product = await repository.GetProductAsync(2);
            Assert.Pass();
        }

        [Test]
        public async Task DeleteProductAsync()
        {
            var repository = _server.Host.Services.GetService<IProductAppServices>();
            //Agregar productos al servicio
            ProductDTO product1 = new ProductDTO
            {
                Id = 1,
                Name = "Takis Fuego",
                WrapperType = "Bolsa de aluminio",
                DescriptionProduct = "Fritura con chile",
                Brand = "Barcel",
                CategoryProduct = "Fritura",
                Price = 10,
                FinalPrice = 11.5
            };
            ProductDTO product2 = new ProductDTO
            {
                Id = 2,
                Name = "Churrumais",
                WrapperType = "Bolsa normal",
                DescriptionProduct = "Fritura con chile y limón",
                Brand = "Sabritas",
                CategoryProduct = "Fritura",
                Price = 9.5,
                FinalPrice = 10.5
            };
            ProductDTO product3 = new ProductDTO
            {
                Id = 3,
                Name = "Chokies",
                WrapperType = "Envoltura de plastico",
                DescriptionProduct = "Galletas con shispas de chocolate",
                Brand = "Marinela",
                CategoryProduct = "Galletas",
                Price = 14.5,
                FinalPrice = 10.5
            };

            await repository.AddProductAsync(product1);
            await repository.AddProductAsync(product2);
            await repository.AddProductAsync(product3);
            //Listar el contenido
            List<Product> list = await repository.GetProductsAsync();
            //Eliminar uno en especifico
            await repository.DeleteProductAsync(2);
            //Volver a listar
            List<Product> list_2 = await repository.GetProductsAsync();

            Assert.Pass();
        }

        [Test]
        public async Task EditProductAsync()
        {
            var repository = _server.Host.Services.GetService<IProductAppServices>();
            //Agregar productos al servicio
            /*
            ProductDTO product1 = new ProductDTO
            {
                Id = 1,
                Name = "Takis Fuego",
                WrapperType = "Bolsa de aluminio",
                DescriptionProduct = "Fritura con chile",
                Brand = "Barcel",
                CategoryProduct = "Fritura",
                Price = 10,
                FinalPrice = 11.5
            };
            ProductDTO product2 = new ProductDTO
            {
                Id = 2,
                Name = "Churrumais",
                WrapperType = "Bolsa normal",
                DescriptionProduct = "Fritura con chile y limón",
                Brand = "Sabritas",
                CategoryProduct = "Fritura",
                Price = 9.5,
                FinalPrice = 10.5
            };
            ProductDTO product3 = new ProductDTO
            {
                Id = 3,
                Name = "Chokies",
                WrapperType = "Envoltura de plastico",
                DescriptionProduct = "Galletas con shispas de chocolate",
                Brand = "Marinela",
                CategoryProduct = "Galletas",
                Price = 14.5,
                FinalPrice = 10.5
            };

            await repository.AddProductAsync(product1);
            await repository.AddProductAsync(product2);
            await repository.AddProductAsync(product3);
            
            */
            await repository.AddAllProductsInMemory();
            //Listar el contenido
            List<Product> list = await repository.GetProductsAsync();
            //Eliminar uno en especifico
            var espera = 0;
            Product product2 = await repository.GetProductAsync(2);
            product2.Description= "Fritura Mexicana con sabor a chile";
            product2.Category = "Botana";
            product2.Price = 11.1;
            await repository.EditProductAsync(product2);
            //Volver a listar
            List<Product> list_2 = await repository.GetProductsAsync();

            Assert.Pass();
        }
    }
}