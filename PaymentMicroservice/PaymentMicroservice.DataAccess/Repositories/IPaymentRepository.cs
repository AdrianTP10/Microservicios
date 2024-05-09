using PaymentMicroservice.Core.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMicroservice.DataAccess.Repositories
{
    public interface IPaymentRepository
    {
        IQueryable<Payment> GetAll();

        Task<Payment> GetAsync(int id);
        Task<Payment> AddAsync(Payment payment);

        Task<Payment> UpdateAsync(Payment payment);

        Task DeleteAsync(int id);
    }
}
