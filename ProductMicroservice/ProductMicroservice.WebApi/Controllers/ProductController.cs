using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using ProductMicroservice.ApplicationServices.Products;
using ProductMicroservice.Core.Products;
using ProductMicroservice.Core.ProductsDTO;
using Newtonsoft.Json;

namespace ProductMicroservice.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppServices _productAppServices;

        public ProductController(IProductAppServices productAppServices)
        {
            _productAppServices = productAppServices;
        }

        //Lista todos los products que hay
        [HttpGet]
        public async Task<List<Product>> GetAll()
        {
            List<Product> products = await _productAppServices.GetProductsAsync();
            return products;
        }

        //Extrae un product en base a un id
        [HttpGet(nameof(GetById))]
        public async Task<string> GetById(int productId)
        {
            bool productExist = await _productAppServices.ExistProductAsync(productId);
            if (!productExist)
            {
                return "The product id '" + productId + "' doesn't exist";
            }
            Product product = await _productAppServices.GetProductAsync(productId);
            string stringProduct = JsonConvert.SerializeObject(product, Formatting.Indented);
            return stringProduct;
        }

        //Agrega un producto
        [HttpPost(nameof(AddProduct))]
        public async Task<int> AddProduct([FromBody] ProductDTO product)
        {
            int id = await _productAppServices.AddProductAsync(product);
            return id;
        }

        //Edita un producto especifico
        [HttpPut(nameof(EditProduct))]
        public async Task<string> EditProduct([FromBody] Product product)
        {
            bool productExist = await _productAppServices.ExistProductAsync(product.Id);
            if (!productExist)
            {
                return "The product id '" + product.Id + "' doesn't exist";
            }

            Product productEdit = await _productAppServices.GetProductAsync(product.Id);
            productEdit.Name = product.Name;
            productEdit.Description = product.Description;
            productEdit.Brand = product.Brand;
            productEdit.Category = product.Category;
            productEdit.Price = product.Price;
            await _productAppServices.EditProductAsync(productEdit);
            return "Product with the id '" + product.Id + "' update with exit! ";
        }

        //Elimina un product en base a un id especifico
        [HttpDelete(nameof(DeleteProduct))]
        public async Task<string> DeleteProduct(int productId)
        {
            bool productExist = await _productAppServices.ExistProductAsync(productId);
            if (!productExist)
            {
                return "The product id '" + productId + "' doesn't exist";
            }
            await _productAppServices.DeleteProductAsync(productId);
            return "Product with the id '" + productId + "' delete with exit! ";
        }
    }
}
