using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Core.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMicroservice.DataAccess.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentMicroserviceContext _context;
        public PaymentRepository(PaymentMicroserviceContext context) { _context = context; }

        public async Task<Payment> AddAsync(Payment payment)
        {
            if (payment == null) throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");

            try
            {
                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(payment)} could not be saved: {ex.Message}");
            }
        }

        public IQueryable<Payment> GetAll()
        {
            try
            {
                return _context.Set<Payment>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve users: {ex.Message}");
            }
        }

        public async Task<Payment> GetAsync(int id)
        {
            var payment = await _context.FindAsync<Payment>(id);
            return payment;
        }

        public async Task<Payment> UpdateAsync(Payment payment)
        {
            if (payment == null) throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");

            try
            {
                _context.Attach(payment);
                _context.Entry(payment).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(payment)} could not be updated: {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await _context.FindAsync<Payment>(id);
            _context.Remove<Payment>(payment);
            await _context.SaveChangesAsync();
        }

        
    }
}
