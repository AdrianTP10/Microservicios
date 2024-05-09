using OrderMicroservice.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.ApplicationServices.Products
{
    public interface IProductAppServices
    {
        Task<Product> GetProductAsync(int productId);
        Task<bool> ExistProductAsync(int productId);
    }
}
