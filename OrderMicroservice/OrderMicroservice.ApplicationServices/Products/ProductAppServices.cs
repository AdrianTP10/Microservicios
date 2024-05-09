using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderMicroservice.Core.Products;
using OrderMicroservice.DataAccess.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.ApplicationServices.Products
{
    public class ProductAppServices : IProductAppServices
    {
        private readonly IRepository<int, Product> _repository;
        public ProductAppServices(IRepository<int, Product> repository)
        {
            _repository = repository;
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
    }
}
