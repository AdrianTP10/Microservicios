using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Users;

namespace UserMicroservice.DataAccess
{
    public class UserMicroserviceContext : DbContext
    {
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public UserMicroserviceContext(DbContextOptions<UserMicroserviceContext> options) : base(options)
        {


        }
    }
}
