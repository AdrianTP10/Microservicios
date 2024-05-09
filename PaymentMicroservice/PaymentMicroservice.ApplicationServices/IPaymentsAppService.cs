using PaymentMicroservice.Core.Payments;
using PaymentMicroservice.Payments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMicroservice.ApplicationServices
{
    public interface IPaymentsAppService
    {
        Task<List<Payment>> GetPaymentsAsync();

        Task<int> AddPaymentAsync(PaymentDto paymentDto);

        Task DeletePaymentAsync(int paymentId);


        Task<Payment> GetPaymentByIdAsync(int paymentId);

        Task EditPaymentAsync(PaymentDto paymentDto);

    }
}
