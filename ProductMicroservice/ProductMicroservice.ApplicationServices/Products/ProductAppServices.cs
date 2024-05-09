using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductMicroservice.Core.Products;
using ProductMicroservice.Core.ProductsDTO;
using ProductMicroservice.DataAccess.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.ApplicationServices.Products
{
    public class ProductAppServices : IProductAppServices
    {
        private readonly IRepository<int, Product> _repository;
        private readonly IMapper _mapper;
        public ProductAppServices(IRepository<int, Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddProductAsync(ProductDTO productdto)
        {
            Product product = _mapper.Map<Product>(productdto);
            await _repository.AddAsync(product);

            // Registro del evento
            Log.Information("The method was executed: AddProductAsync");

            return product.Id;
        }

        public async Task DeleteProductAsync(int productId)
        {
            // Registro del evento
            Log.Information("The method was executed: DeleteProductAsync");

            await _repository.DeleteAsync(productId);
        }

        public async Task EditProductAsync(Product product)
        {
            // Registro del evento
            Log.Information("The method was executed: EditProductAsync");

            await _repository.UpdateAsync(product);
        }

        public async Task<List<Product>> GetProductsAsync()
        {

            // Registro del evento
            Log.Information("The method was executed: GetProductsAsync");

            return await _repository.GetAll().ToListAsync();
        }

        public async Task<Product> GetProductAsync(int productId)
        {

            // Registro del evento
            Log.Information("The method was executed: GetProductAsync");

            return await _repository.GetAsync(productId);
        }

        public async Task<bool> ExistProductAsync(int productId)
        {
            // Registro del evento
            Log.Information("The method was executed: ExistProductAsync");

            Product product = await _repository.GetAsync(productId);
            if (product == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task AddAllProductsInMemory()
        {

            Product product1 = new Product
            {
                Id = 1,
                Name = "Takis Fuego",
                Description = "Fritura con chile",
                Brand = "Barcel",
                Category = "Fritura",
                Price = 10
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
            await _repository.AddAsync(product1);
            await _repository.AddAsync(product2);
            await _repository.AddAsync(product3);
        }
    }
}
