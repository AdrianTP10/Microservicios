using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.DataAccess
{
    public class ProductMicroserviceContext : IdentityDbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public ProductMicroserviceContext(DbContextOptions options) : base(options)
        {
            

        }
    }
}
