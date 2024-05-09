using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Core.Payments;
using PaymentMicroservice.DataAccess.Repositories;
using PaymentMicroservice.Payments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMicroservice.ApplicationServices
{
    public class PaymentsAppService : IPaymentsAppService
    {
        private readonly IPaymentRepository  _repository;
        private readonly IMapper _mapper;

        public PaymentsAppService(IPaymentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _repository.GetAsync(paymentId);
        }

        public async Task<List<Payment>> GetPaymentsAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<int> AddPaymentAsync(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            await _repository.AddAsync(payment);
            return payment.Id;
        }

        public async Task DeletePaymentAsync(int paymentId)
        {
            await _repository.DeleteAsync(paymentId);
        }

        public async Task EditPaymentAsync(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            await _repository.UpdateAsync(payment);
        }

        
    }
}
