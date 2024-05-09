using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Core.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMicroservice.DataAccess
{
    public class PaymentMicroserviceContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public virtual DbSet<Payment> Payments { get; set; }
        
        public PaymentMicroserviceContext(DbContextOptions<PaymentMicroserviceContext> options) : base(options)
        {


        }
    }
}
