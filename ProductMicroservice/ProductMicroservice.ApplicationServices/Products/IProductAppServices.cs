using ProductMicroservice.Core.Products;
using ProductMicroservice.Core.ProductsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.ApplicationServices.Products
{
    public interface IProductAppServices
    {
        Task<List<Product>> GetProductsAsync();

        Task<int> AddProductAsync(ProductDTO productdto);

        Task DeleteProductAsync(int productId);
        Task<Product> GetProductAsync(int productId);

        Task EditProductAsync(Product product);
        Task<bool> ExistProductAsync(int productId);

        Task AddAllProductsInMemory();
    }
}
