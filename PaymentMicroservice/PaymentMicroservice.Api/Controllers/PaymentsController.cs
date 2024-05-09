using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentMicroservice.ApplicationServices;
using PaymentMicroservice.Core.Payments;
using PaymentMicroservice.Payments.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentMicroservice.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsAppService _paymentsAppService; 
        readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentsAppService paymentsAppService, ILogger<PaymentsController> logger)
        {
            _paymentsAppService = paymentsAppService;
            _logger = logger;
        }


        // GET: api/<PaymentsController>
        [HttpGet]
        public async Task<List<Payment>> GetAll()
        {
            return await _paymentsAppService.GetPaymentsAsync();
        }

        // GET api/<PaymentsController>/5
        [HttpGet("{id}")]
        public async Task<Payment> GetById(int id)
        {
            return await _paymentsAppService.GetPaymentByIdAsync(id);
        }

        // POST api/<PaymentsController>
        [HttpPost]
        public async Task<Int32> Insert([FromBody] PaymentDto paymentDto)
        {
            var result = await _paymentsAppService.AddPaymentAsync(paymentDto);
            return result;

        }

        // PUT api/<PaymentsController>/5
        [HttpPut]
        public async Task Put([FromBody] PaymentDto paymentDto)
        {
            await _paymentsAppService.EditPaymentAsync(paymentDto);
        }

        // DELETE api/<PaymentsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _paymentsAppService.DeletePaymentAsync(id);

        }
    }
}
