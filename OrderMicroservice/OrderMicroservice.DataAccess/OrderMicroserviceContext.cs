using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Core.Orders;
using OrderMicroservice.Core.Products;
using OrderMicroservice.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.DataAccess
{
    public class OrderMicroserviceContext : IdentityDbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public OrderMicroserviceContext(DbContextOptions options) : base(options)
        {


        }
    }
}
